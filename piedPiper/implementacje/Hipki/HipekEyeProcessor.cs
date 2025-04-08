using static PipelineSystem;

namespace piedPiper.implementacje.Hipki
{
    public class HipekEyeProcessor : IProcessor<ocenionyHipek, ocenionyHipek>
    {
        private float waga = 0;

        public HipekEyeProcessor(float waga)
        {
            this.waga = waga;
        }
        public ocenionyHipek Process(ocenionyHipek input, Context context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.hipek.eyeColor == "niebieski")
            {
                input.ocena += 1 * waga;
            }
            return input;
        }
    }





}
