using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class IntToStringProcessor : IProcessor<int, string>
        {
            private readonly string _prefix;
            public IntToStringProcessor(string prefix = "") { _prefix = prefix; }
            public string Process(int input, Context context)
            {
                context.Log($"IntToStringProcessor: Converting {input} to string with prefix '{_prefix}'.");
                return _prefix + input.ToString();
            }
        }
}