using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class ToUpperCaseProcessor : IProcessor<string, string>
        {
            public string Process(string input, Context context)
            {
                context.Log($"ToUpperCaseProcessor: Converting '{input}' to uppercase.");
                return input.ToUpper();
            }
        }
}