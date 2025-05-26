public partial class PipelineSystem
{
    // --- 3. PipelineBackwards Interface (Nested) ---
    public interface IPipeline<InputType, OutputType>
    {
        /// <summary>
        /// Executes the full pipeline, creating a new context.
        /// </summary>
        OutputType Execute(InputType input, out Context context);

        /// <summary>
        /// Executes the pipeline steps using a provided context.
        /// (Could be internal or protected if needed)
        /// </summary>
        OutputType ExecuteSubPipeline(InputType input, Context context);
    }
}