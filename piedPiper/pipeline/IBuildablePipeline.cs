// --- Inside PipelineSystem Class ---

public partial class PipelineSystem
{



    public interface IBuildablePipeline<InputType, OutputType> : IPipeline<InputType, OutputType>
    {
        // Crucially, this returns the IBuildablePipeline interface itself, enabling chaining
        IBuildablePipeline<InputType, NextOutputType> AppendProcessor<NextOutputType>(IProcessor<OutputType, NextOutputType> processor);
    }
}