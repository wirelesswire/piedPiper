using System.Drawing;
using static PipelineSystem;
using System.Drawing.Imaging;
//using System.Drawing.Common, Version = 0.0.0.0, Culture = neutral, PublicKeyToken = cc7b13ffcd2ddd51;

namespace piedPiper.implementacje.obrazy
{
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






}
