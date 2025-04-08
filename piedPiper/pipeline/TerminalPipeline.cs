public partial class PipelineSystem
{
    // Represents the first step in the pipeline
    public class TerminalPipeline<InputType, OutputType> : PipelineBase<InputType, InputType, OutputType>
    {
        public TerminalPipeline(IProcessor<InputType, OutputType> processor)
            : base(processor) // Pass processor to base constructor
        { }

        /// <summary>
        /// Executes only the first processor in the chain.
        /// </summary>
        public override OutputType ExecuteSubPipeline(InputType input, Context context)
        {
            context.Log($"Executing terminal processor: {currentProcessor.GetType().Name}");
            var result = currentProcessor.Process(input, context);
            context.Log($"Terminal processor finished.");
            return result;
        }
    }
}