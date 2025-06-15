// --- Inside PipelineSystem Class ---

// --- Inside PipelineSystem Class ---



using piedPiper.pipeline.piedPiper.pipeline;

namespace piedPiper.pipeline
{

    public abstract class PipelineBase<InputType, ProcessorInputType, OutputType> : IBuildablePipeline<InputType, OutputType>
    {
        protected IProcessor<ProcessorInputType, OutputType> currentProcessor;

        public PipelineBase(IProcessor<ProcessorInputType, OutputType> processor)
        {
            if (processor == null) throw new ArgumentNullException(nameof(processor));
            currentProcessor = processor;
        }


        public IBuildablePipeline<InputType, ProcessorOutputType> AppendProcessor<ProcessorOutputType>(IProcessor<OutputType, ProcessorOutputType> processor)
        {
            return new PipelineBackwards<InputType, OutputType, ProcessorOutputType>(processor, this);
        }

        public IBuildablePipeline<InputType, IEnumerable<TBranchOutput>> Split<TBranchOutput>(
            params Func<IBuildablePipeline<OutputType, OutputType>, IBuildablePipeline<OutputType, TBranchOutput>>[] branchBuilders)
        {
            if (branchBuilders == null || !branchBuilders.Any())
            {
                throw new ArgumentException("At least one branch builder must be provided for a split operation.", nameof(branchBuilders));
            }

            var branchPipelines = new List<IPipeline<OutputType, TBranchOutput>>();

            foreach (var builderFunc in branchBuilders)
            {
                // Create a starting point for each branch using an IdentityProcessor.
                // This allows the branch builder to chain off of the current pipeline's OutputType.
                var seedPipeline = Pipeline.Create(new IdentityProcessor<OutputType>());

                // Build the full pipeline for this specific branch
                var branchPipeline = builderFunc(seedPipeline);
                branchPipelines.Add(branchPipeline);
            }

            // Create and append the SplitProcessor, which takes the current pipeline's output
            // and executes all constructed branch pipelines in parallel.
            var splitProcessor = new SplitProcessor<OutputType, TBranchOutput>(branchPipelines);
            return AppendProcessor(splitProcessor);
        }

        public OutputType Execute(InputType input, out Context context)
        {
            context = new Context();
            context.ProcessStartedAt = DateTime.Now;
            context.Log("Pipeline execution started.");
            OutputType result;
            try
            {
                result = ExecuteSubPipeline(input, context);
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