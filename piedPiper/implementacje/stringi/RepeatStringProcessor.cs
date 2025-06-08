using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class RepeatStringProcessor : IProcessor<string, string>
    {
        int times;
        public RepeatStringProcessor(int times = 3 )
        {
            this.times = times;
        }

        public string Process(string input, Context context)
        {
            context.Log($"Processing '{input}' in RepeatStringProcessor.");
            if (string.IsNullOrEmpty(input))
            {
                context.Log($"Input is null or empty, returning as is.");
                return input; // Example of conditional processing
            }
            //string result = input + input + input; // Example: Repeat 3 times
            string result = "";
            
            for (int i = 0; i < times; i++)
            {
                result += input;
            }

            context.Log($"Result: {result}");
            return result;
        }
    }
}