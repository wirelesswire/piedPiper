using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PipelineSystem;

using System;
using System.Collections.Generic;
using System.Drawing; // Required using
using System.Drawing.Imaging; // Required using
using System.IO; // Required usingusing System;
using System.Collections.Generic;
using System.Drawing; // Required using
using System.Drawing;
using System.Drawing.Imaging; // Required using
using System.IO; // Required using
//using System.Drawing.Common, Version = 0.0.0.0, Culture = neutral, PublicKeyToken = cc7b13ffcd2ddd51;

namespace piedPiper
{
    // --- NEW IMAGE PROCESSORS ---

    /// <summary>
    /// Reads an image file from a path and converts it to a Bitmap.
    /// Input: string (file path)
    /// Output: Bitmap
    /// </summary>
    public class PathToBitmapProcessor : IProcessor<string, Bitmap>
    {
        public Bitmap Process(string imagePath, Context context)
        {
            context.Log($"Attempting to load image from: {imagePath}");
            if (!File.Exists(imagePath))
            {
                string errorMsg = $"Image file not found: {imagePath}";
                context.Log($"ERROR: {errorMsg}");
                throw new FileNotFoundException(errorMsg, imagePath);
            }

            try
            {
                // Load the image using System.Drawing
                // IMPORTANT: The Bitmap needs to be disposed of eventually.
                // The pipeline passes ownership, so the final handler or
                // a subsequent processor that creates a *new* bitmap is responsible.
                Bitmap loadedBitmap = new Bitmap(imagePath);
                context.Log($"Successfully loaded image. Dimensions: {loadedBitmap.Width}x{loadedBitmap.Height}, PixelFormat: {loadedBitmap.PixelFormat}");
                return loadedBitmap;
            }
            catch (Exception ex)
            {
                context.Log($"ERROR loading image: {ex.Message}");
                throw; // Re-throw to indicate pipeline failure
            }
        }
    }

    /// <summary>
    /// Applies a convolution kernel to a Bitmap image.
    /// Input: Bitmap
    /// Output: Bitmap (newly created convolved image)
    /// Note: Uses GetPixel/SetPixel for simplicity, which is slow. Use LockBits for performance.
    /// </summary>
    public class ConvolutionProcessor : IProcessor<Bitmap, Bitmap>
    {
        private readonly float[,] _kernel;
        private readonly float _factor; // Optional factor (often 1.0)
        private readonly float _bias;   // Optional bias (often 0.0)
        private readonly string _kernelName; // For logging

        public ConvolutionProcessor(string kernelName, float[,] kernel, float factor = 1.0f, float bias = 0.0f)
        {
            if (kernel == null) throw new ArgumentNullException(nameof(kernel));
            if (kernel.GetLength(0) % 2 == 0 || kernel.GetLength(1) % 2 == 0)
                throw new ArgumentException("Kernel dimensions must be odd.", nameof(kernel));

            _kernelName = kernelName;
            _kernel = kernel;
            _factor = factor;
            _bias = bias;
        }

        public Bitmap Process(Bitmap inputBitmap, Context context)
        {
            if (inputBitmap == null)
            {
                context.Log("ERROR: Input bitmap is null for ConvolutionProcessor.");
                // Depending on requirements, either return null or throw
                throw new ArgumentNullException(nameof(inputBitmap));
            }

            context.Log($"Applying '{_kernelName}' convolution kernel ({_kernel.GetLength(0)}x{_kernel.GetLength(1)}).");
            context.Log("WARNING: Using slow GetPixel/SetPixel. Use LockBits for production code.");

            int width = inputBitmap.Width;
            int height = inputBitmap.Height;
            // Create a new bitmap for the output
            PixelFormat outputPixelFormat = PixelFormat.Format24bppRgb;
            context.Log($"Creating output bitmap with format: {outputPixelFormat}");
            Bitmap outputBitmap = new Bitmap(width, height, outputPixelFormat);

            //Bitmap outputBitmap = new Bitmap(width, height, inputBitmap.PixelFormat);

            int kernelWidth = _kernel.GetLength(1);
            int kernelHeight = _kernel.GetLength(0);
            int kernelOffsetX = kernelWidth / 2;
            int kernelOffsetY = kernelHeight / 2;

            // --- Simple GetPixel/SetPixel Convolution ---
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float sumR = 0, sumG = 0, sumB = 0;

                    // Apply kernel
                    for (int ky = 0; ky < kernelHeight; ky++)
                    {
                        for (int kx = 0; kx < kernelWidth; kx++)
                        {
                            int sampleX = x + kx - kernelOffsetX;
                            int sampleY = y + ky - kernelOffsetY;

                            // Clamp coordinates to handle borders
                            sampleX = Math.Max(0, Math.Min(width - 1, sampleX));
                            sampleY = Math.Max(0, Math.Min(height - 1, sampleY));

                            Color pixel = inputBitmap.GetPixel(sampleX, sampleY);
                            float weight = _kernel[ky, kx];

                            sumR += pixel.R * weight;
                            sumG += pixel.G * weight;
                            sumB += pixel.B * weight;
                        }
                    }

                    // Apply factor and bias
                    sumR = sumR * _factor + _bias;
                    sumG = sumG * _factor + _bias;
                    sumB = sumB * _bias + _bias; // Corrected: Apply bias to sumB as well

                    // Clamp results to 0-255
                    byte finalR = (byte)Math.Max(0, Math.Min(255, sumR));
                    byte finalG = (byte)Math.Max(0, Math.Min(255, sumG));
                    byte finalB = (byte)Math.Max(0, Math.Min(255, sumB));

                    outputBitmap.SetPixel(x, y, Color.FromArgb(finalR, finalG, finalB));
                }
            }
            // --- End Convolution ---

            // The input bitmap was used but not modified.
            // A *new* bitmap is returned. The caller or next step manages this new bitmap.
            context.Log($"Convolution '{_kernelName}' applied successfully.");
            return outputBitmap;
        }
    }

    /// <summary>
    /// Applies a specific Laplacian filter (edge detection) using ConvolutionProcessor logic.
    /// Input: Bitmap
    /// Output: Bitmap (newly created filtered image)
    /// </summary>
    public class LaplacianProcessor : IProcessor<Bitmap, Bitmap>
    {
        // Example Laplacian Kernel (detects edges)
        private static readonly float[,] LaplacianKernel = new float[,]
        {
            { 0,  1,  0 },
            { 1, -4,  1 },
            { 0,  1,  0 }
        };
        // Alternative kernel:
        /*
        private static readonly float[,] LaplacianKernel = new float[,]
        {
            { -1, -1, -1 },
            { -1,  8, -1 },
            { -1, -1, -1 }
        };
        */


        // Could reuse ConvolutionProcessor internally, but implementing directly for clarity
        private readonly ConvolutionProcessor _internalProcessor;

        public LaplacianProcessor()
        {
            // Or implement the logic directly here (copy/paste from ConvolutionProcessor
            // but with the fixed kernel). Using the internal processor is cleaner:
            _internalProcessor = new ConvolutionProcessor("Laplacian", LaplacianKernel);
        }

        public Bitmap Process(Bitmap inputBitmap, Context context)
        {
            context.Log("Applying Laplacian Filter...");
            // Delegate the work to the internal ConvolutionProcessor
            var result = _internalProcessor.Process(inputBitmap, context);
            context.Log("Laplacian Filter applied.");
            return result;
        }
    }


    /// <summary>
    /// Saves a Bitmap object to a file with a specified extension and base name.
    /// Generates a unique filename using the context's UniqueToken.
    /// Input: Bitmap
    /// Output: string (full path of the saved file)
    /// </summary>
    public class BitmapSaveProcessor : IProcessor<Bitmap, string>
    {
        private readonly string _outputDirectory;
        private readonly string _baseFilename;
        private readonly string _extension; // e.g., ".png", ".jpg"
        private readonly ImageFormat _imageFormat;

        /// <summary>
        /// Initializes the processor.
        /// </summary>
        /// <param name="outputDirectory">The directory to save the image in.</param>
        /// <param name="baseFilename">Base name for the file (unique ID and extension will be added).</param>
        /// <param name="extension">The desired file extension (e.g., ".png", ".jpg", ".bmp"). Determines the save format.</param>
        public BitmapSaveProcessor(string outputDirectory, string baseFilename, string extension)
        {
            if (string.IsNullOrWhiteSpace(outputDirectory))
                throw new ArgumentNullException(nameof(outputDirectory));
            if (string.IsNullOrWhiteSpace(baseFilename))
                throw new ArgumentNullException(nameof(baseFilename));
            if (string.IsNullOrWhiteSpace(extension) || !extension.StartsWith("."))
                throw new ArgumentException("Extension must start with a dot (e.g., '.png').", nameof(extension));

            _outputDirectory = outputDirectory;
            _baseFilename = baseFilename;
            _extension = extension.ToLowerInvariant(); // Normalize extension

            // Determine ImageFormat based on extension
            _imageFormat = GetImageFormatFromExtension(_extension);
        }

        private ImageFormat GetImageFormatFromExtension(string ext)
        {
            switch (ext)
            {
                case ".png": return ImageFormat.Png;
                case ".jpg":
                case ".jpeg": return ImageFormat.Jpeg;
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".tif":
                case ".tiff": return ImageFormat.Tiff;
                default:
                    Console.WriteLine($"Warning: Unknown extension '{ext}'. Defaulting to PNG format."); // Log warning
                    return ImageFormat.Png; // Default to PNG for unknown formats
            }
        }

        public string Process(Bitmap inputBitmap, Context context)
        {
            if (inputBitmap == null)
            {
                context.Log("ERROR: Input bitmap is null for BitmapSaveProcessor. Cannot save.");
                // Decide: throw or return null? Returning null might break subsequent steps
                // if they expect a valid path. Throwing is safer here.
                throw new ArgumentNullException(nameof(inputBitmap), "Input Bitmap cannot be null for saving.");
            }

            try
            {
                // Ensure output directory exists
                if (!Directory.Exists(_outputDirectory))
                {
                    context.Log($"Creating output directory: {_outputDirectory}");
                    Directory.CreateDirectory(_outputDirectory);
                }

                // Construct unique filename
                string filename = $"{_baseFilename}_{context.UniqueToken}{_extension}";
                string fullPath = Path.Combine(_outputDirectory, filename);

                context.Log($"Attempting to save Bitmap ({inputBitmap.Width}x{inputBitmap.Height}) to: {fullPath} with format {_imageFormat}");

                // Save the bitmap
                inputBitmap.Save(fullPath, _imageFormat);

                context.Log($"Bitmap successfully saved to: {fullPath}");

                // Return the full path where the image was saved
                return fullPath;
            }
            catch (Exception ex)
            {
                context.Log($"ERROR saving bitmap: {ex.Message}");
                // Re-throw the exception to indicate pipeline failure
                throw new IOException($"Failed to save bitmap to directory '{_outputDirectory}'. Reason: {ex.Message}", ex);
            }
            // Note: We DO NOT dispose the inputBitmap here. The caller or a subsequent
            // step (or final cleanup) is responsible for managing the bitmap's lifecycle.
            // This processor just uses it to save a copy to disk.
        }
    }






}
