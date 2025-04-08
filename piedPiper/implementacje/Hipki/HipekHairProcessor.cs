using static PipelineSystem;

namespace piedPiper.implementacje.Hipki
{
    public class HipekHairProcessor : IProcessor<ocenionyHipek, ocenionyHipek>
    {
        private float waga = 0;

        public HipekHairProcessor(int waga)
        {
            this.waga = waga;
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
