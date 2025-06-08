


namespace piedPiper.pipeline
{

    public interface IBuildablePipeline<InputType, OutputType> : IPipeline<InputType, OutputType>
    {
        IBuildablePipeline<InputType, NextOutputType> AppendProcessor<NextOutputType>(IProcessor<OutputType, NextOutputType> processor);

        /// <summary>
        /// Splits the pipeline execution into multiple parallel branches.
        /// Each branch starts with the current pipeline's OutputType as its input.
        /// The result of this split operation is a pipeline that outputs an IEnumerable of results from all branches.
        /// </summary>
        /// <typeparam name="TBranchOutput">The common output type for all parallel branches.</typeparam>
        /// <param name="branchBuilders">
        /// Functions that define each parallel branch. Each function receives a seed pipeline
        /// (which takes the current pipeline's OutputType as input and outputs that same type initially)
        /// and should return a complete pipeline for that branch, ending with TBranchOutput.
        /// </param>
        /// <returns>A new buildable pipeline whose output type is an IEnumerable of TBranchOutput.</returns>
        IBuildablePipeline<InputType, IEnumerable<TBranchOutput>> Split<TBranchOutput>(
            params Func<IBuildablePipeline<OutputType, OutputType>, IBuildablePipeline<OutputType, TBranchOutput>>[] branchBuilders);
    }
}