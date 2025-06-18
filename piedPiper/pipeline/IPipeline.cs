

namespace piedPiper.pipeline
{
    public interface IPipeline<InputType, OutputType>
    {
        /// <summary>
        /// Executes the full pipeline, creating a new context.
        /// </summary>
        OutputType Execute(InputType input, out Context context);

        /// <summary>
        /// Executes the pipeline steps using a provided context.
        /// </summary>
        OutputType ExecuteSubPipeline(InputType input, Context context);
    }
}