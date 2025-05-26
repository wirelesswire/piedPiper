// --- Inside PipelineSystem Class ---

public partial class PipelineSystem
{

    public static class Pipeline
    {
        
        public static IBuildablePipeline<InputType, OutputType> Create<InputType, OutputType>(IProcessor<InputType, OutputType> processor)
        {
            return new TerminalPipeline<InputType, OutputType>(processor);
        }
    }
    public class PipelineBackwards<InputType, ProcessorInputType, OutputType> : PipelineBase<InputType, ProcessorInputType, OutputType>
    {
        // Reference to the previous part of the pipeline
        private readonly IPipeline<InputType, ProcessorInputType> previousPipeline;

        public PipelineBackwards(
            IProcessor<ProcessorInputType, OutputType> processor,
            IPipeline<InputType, ProcessorInputType> previousPipeline)
            : base(processor) // Pass processor to base constructor
        {
            if (previousPipeline == null) throw new ArgumentNullException(nameof(previousPipeline));
            this.previousPipeline = previousPipeline;
        }

        /// <summary>
        /// Executes the previous pipeline steps first, then the current processor.
        /// </summary>
        public override OutputType ExecuteSubPipeline(InputType input, Context context)
        {
            context.Log($"Executing previous pipeline segment(s)...");
            // 1. Execute the pipeline built so far
            var previousPipelineResult = previousPipeline.ExecuteSubPipeline(input, context);

            context.Log($"Executing intermediate processor: {currentProcessor.GetType().Name}");
            // 2. Process the result with the current processor
            var result = currentProcessor.Process(previousPipelineResult, context);
            context.Log($"Intermediate processor finished.");
            return result;
        }
    }

}