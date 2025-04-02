// --- Inside PipelineSystem Class ---

public partial class PipelineSystem
{
    //public class RepeatStringProcessor : IProcessor<string, string>
    //{
    //    public string Process(string input, Context context)
    //    {
    //        context.Log($"Processing '{input}' in RepeatStringProcessor.");
    //        if (string.IsNullOrEmpty(input))
    //        {
    //            context.Log($"Input is null or empty, returning as is.");
    //            return input; // Example of conditional processing
    //        }
    //        string result = input + input + input; // Example: Repeat 3 times
    //        context.Log($"Result: {result}");
    //        return result;
    //    }
    //}







    // 1. Define a new interface that includes AppendProcessor
    public interface IBuildablePipeline<InputType, OutputType> : IPipeline<InputType, OutputType>
    {
        // Crucially, this returns the IBuildablePipeline interface itself, enabling chaining
        IBuildablePipeline<InputType, NextOutputType> AppendProcessor<NextOutputType>(IProcessor<OutputType, NextOutputType> processor);
    }
}