using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{


    public class CombineStringsJoinProcessor : IProcessor<IEnumerable<string>, string>
    {
        private readonly string _separator;
        public CombineStringsJoinProcessor(string separator = ", ")
        {
            _separator = separator;
        }
        public string Process(IEnumerable<string> input, Context context)
        {
            context.Log($"CombineStringsJoinProcessor: Joining strings with separator '{_separator}'.");
            return string.Join(_separator, input);
        }
    }
}