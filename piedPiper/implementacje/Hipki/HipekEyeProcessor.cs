using FuzzySharp.Operators.SNorm;
using static PipelineSystem;

namespace piedPiper.implementacje.Hipki
{
    public class HipekEyeProcessor : IProcessor<ocenionyHipek, ocenionyHipek>
    {

        public HipekEyeProcessor()
        {
        }
        public NormOperationAlgebraicSum<float> NormOperationAlgebraicSum { get; set; } = new NormOperationAlgebraicSum<float>();

        public ocenionyHipek Process(ocenionyHipek input, Context context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.hipek.eyeColor == "niebieski")
            {
                input.ocena =  NormOperationAlgebraicSum.Calculate(input.ocena,1);
            }
            else
            {
                input.ocena = NormOperationAlgebraicSum.Calculate(input.ocena, 0);
            }

            return input;
        }
    }





}
