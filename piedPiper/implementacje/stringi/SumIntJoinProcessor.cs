using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class SumIntJoinProcessor : IProcessor<IEnumerable<int>, int>
    {
        public int Process(IEnumerable<int> input, Context context)
        {
            context.Log("SumIntJoinProcessor: Summing integers.");
            return input.Sum();
        }
    }
}