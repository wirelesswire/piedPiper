using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class ReverseStringProcessor : IProcessor<string, string>
        {
            public string Process(string input, Context context)
            {
                context.Log($"ReverseStringProcessor: Reversing '{input}'.");
                char[] charArray = input.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }
        }
}