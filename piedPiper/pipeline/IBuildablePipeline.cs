// --- Inside PipelineSystem Class ---

public partial class PipelineSystem { 
    public interface IBuildablePipeline<InputType, OutputType> : IPipeline<InputType, OutputType>
    {
        IBuildablePipeline<InputType, NextOutputType> AppendProcessor<NextOutputType>(IProcessor<OutputType, NextOutputType> processor);
    }
}