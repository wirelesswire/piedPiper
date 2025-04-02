// --- Inside PipelineSystem Class ---

public partial class PipelineSystem
{
    // 5. Modify Static Factory Class
    public static class Pipeline
    {
        // CHANGE THE RETURN TYPE HERE to IBuildablePipeline
        public static IBuildablePipeline<InputType, OutputType> Create<InputType, OutputType>(IProcessor<InputType, OutputType> processor)
        {
            return new TerminalPipeline<InputType, OutputType>(processor);
        }
    }
}