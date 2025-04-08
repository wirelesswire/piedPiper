// --- Inside PipelineSystem Class ---


public partial class PipelineSystem
{
    // --- 8. Example Processors (Nested) ---
    public class FloatToStringProcessor : IProcessor<float, string>
    {
        public string Process(float input, Context context)
        {
            context.Log($"Processing {input} in FloatToStringProcessor.");
            string result = input.ToString(); // Example: Simple conversion
            context.Log($"Result: {result}");
            return result;
        }
    }
}