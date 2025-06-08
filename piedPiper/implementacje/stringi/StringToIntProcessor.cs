using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class StringToIntProcessor : IProcessor<string, int>
        {
            public int Process(string input, Context context)
            {
                context.Log($"StringToIntProcessor: Converting '{input}' to length.");
                return input.Length;
            }
        }
}