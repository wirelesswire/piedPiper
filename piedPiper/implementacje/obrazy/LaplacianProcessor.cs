using System.Drawing;
using static PipelineSystem;
//using System.Drawing.Common, Version = 0.0.0.0, Culture = neutral, PublicKeyToken = cc7b13ffcd2ddd51;
using piedPiper.pipeline;

namespace piedPiper.implementacje.obrazy
{
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






}
