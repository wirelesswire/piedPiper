using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PipelineSystem;

using System;
using System.Collections.Generic;
using System.Drawing.Imaging; // Required using
using System.IO; // Required usingusing System;
using System.Collections.Generic;
using System.IO;
//using System.Drawing.Common, Version = 0.0.0.0, Culture = neutral, PublicKeyToken = cc7b13ffcd2ddd51;

namespace piedPiper.implementacje.obrazy
{
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
