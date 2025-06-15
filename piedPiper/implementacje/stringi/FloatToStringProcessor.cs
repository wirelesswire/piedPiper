using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class FloatToStringProcessor : IProcessor<float, string>
    {
        public string Process(float input, Context context)
        {
            context.Log($"Processing {input} in FloatToStringProcessor.");
            string result = input.ToString(); 
            context.Log($"Result: {result}");
            return result;
        }
    }
}