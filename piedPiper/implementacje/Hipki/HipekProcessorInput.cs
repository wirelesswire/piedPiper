using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.Hipki
{
    public class HipekProcessorInput : IProcessor<Hipek, ocenionyHipek>
    {
        public ocenionyHipek Process(Hipek input, Context context)
        {
            return new ocenionyHipek(input, 0);
        }

    }
}
