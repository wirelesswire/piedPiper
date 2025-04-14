using static PipelineSystem;

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
                input.ocena += 1 ;
            }
            return input;
        }
    }





}
