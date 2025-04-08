// --- Inside PipelineSystem Class ---

public partial class PipelineSystem
{
    // 2. Modify PipelineBase
    // Make it implement the new interface
    public abstract class PipelineBase<InputType, ProcessorInputType, OutputType> : IBuildablePipeline<InputType, OutputType>
    {
        protected IProcessor<ProcessorInputType, OutputType> currentProcessor;

        public PipelineBase(IProcessor<ProcessorInputType, OutputType> processor)
        {
            if (processor == null) throw new ArgumentNullException(nameof(processor));
            currentProcessor = processor;
        }

        // CHANGE THE RETURN TYPE HERE to IBuildablePipeline
        public IBuildablePipeline<InputType, ProcessorOutputType> AppendProcessor<ProcessorOutputType>(IProcessor<OutputType, ProcessorOutputType> processor)
        {
            // The concrete Pipeline class will implement IBuildablePipeline
            return new Pipeline<InputType, OutputType, ProcessorOutputType>(processor, this);
        }

        // Execute and ExecuteSubPipeline remain as they implement the base IPipeline part
        public OutputType Execute(InputType input, out Context context)
        {
            context = new Context();
            context.ProcessStartedAt = DateTime.Now;
            context.Log("Pipeline execution started.");
            OutputType result;
            try
            {
                result = ExecuteSubPipeline(input, context);
                context.Log("Pipeline execution finished successfully.");
            }
            catch (Exception ex)
            {
                context.Log($"Pipeline execution failed: {ex.Message}");
                context.ProcessEndedAt = DateTime.Now;
                throw;
            }
            context.ProcessEndedAt = DateTime.Now;
            context.Log($"Total execution time: {context.ProcessTimeInMilliseconds} ms.");
            return result;
        }

        public abstract OutputType ExecuteSubPipeline(InputType input, Context context);
    }
}