using FuzzySharp.Operators.SNorm;
using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.Hipki
{
    public class HipekHairProcessor : IProcessor<ocenionyHipek, ocenionyHipek>
    {

        public HipekHairProcessor()
        {
        }
        public ocenionyHipek Process(ocenionyHipek input, Context context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.hipek.eyeColor == "blond")
            {
                //input.ocena += 1 * waga;
                input.ocena = NormOperationAlgebraicSum<float>.Calculate(input.ocena, 1);
            }
            else
            {
                //input.ocena += 0 * waga;
                input.ocena = NormOperationAlgebraicSum<float>.Calculate(input.ocena, 0);
            }
            return input;
        }
    }





}
