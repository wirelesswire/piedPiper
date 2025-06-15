using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class StringLengthProcessor : IProcessor<string, int> 
    {
        public int Process(string input, Context context)
        {
            context.Log($"Processing '{input}' in StringLengthProcessor.");
            if (input == null)
            {
                context.Log("Input string is null, returning length 0.");
                return 0;
            }
            int length = input.Length;
            context.Log($"Calculated length: {length}");
            return length;
        }
    }
}