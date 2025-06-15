using System.Drawing;
using static PipelineSystem;
//using System.Drawing.Common, Version = 0.0.0.0, Culture = neutral, PublicKeyToken = cc7b13ffcd2ddd51;
using piedPiper.pipeline;

namespace piedPiper.implementacje.obrazy
{
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






}
