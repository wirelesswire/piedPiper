using static PipelineSystem;

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
                input.ocena += 1 * waga;
            }
            return input;
        }
    }





}
