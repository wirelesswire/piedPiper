using FuzzySharp.Operators.SNorm;
using static PipelineSystem;
using piedPiper.pipeline;
namespace piedPiper.implementacje.Hipki
{
    public class HipekEyeProcessor : IProcessor<ocenionyHipek, ocenionyHipek>
    {

        public HipekEyeProcessor()
        {
        }
        public ocenionyHipek Process(ocenionyHipek input, Context context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.hipek.eyeColor == "niebieski")
            {
                input.ocena =  NormOperationAlgebraicSum<float>.Calculate(input.ocena,1);
            }
            else
            {
                input.ocena = NormOperationAlgebraicSum<float>.Calculate(input.ocena, 0);
            }

            return input;
        }
    }





}
