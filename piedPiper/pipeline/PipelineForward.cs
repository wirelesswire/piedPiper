using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PipelineSystem;

namespace piedPiper.pipeline
{

    // The "forward" version of the pipeline segment.
    // Represents adding a processor step (IntermediateType -> OutputType)
    // after a previous pipeline segment (InputType -> IntermediateType).
    // The generics are named to reflect the flow:
    // InputType -> IntermediateType -> OutputType
    public class PipelineForward<InputType, IntermediateType, OutputType> : PipelineBase<InputType, IntermediateType, OutputType>
    {
        // Reference to the previous part of the pipeline.
        // This previous part takes the initial InputType and produces the IntermediateType
        // which will be the input to the current processor.
        private readonly IPipeline<InputType, IntermediateType> previousPipeline;

        /// <summary>
        /// Constructs a new forward pipeline segment.
        /// </summary>
        /// <param name="processor">The processor for this specific step (takes IntermediateType, outputs OutputType).</param>
        /// <param name="previousPipeline">The preceding pipeline segment (takes InputType, outputs IntermediateType).</param>
        public PipelineForward(
            IProcessor<IntermediateType, OutputType> processor, // The processor for this step (input is IntermediateType, output is OutputType)
            IPipeline<InputType, IntermediateType> previousPipeline) // The previous segment (takes InputType, outputs IntermediateType)
            : base(processor) // Pass the current step's processor to the base class
        {
            if (previousPipeline == null) throw new ArgumentNullException(nameof(previousPipeline));
            this.previousPipeline = previousPipeline;
        }

        /// <summary>
        /// Executes the previous pipeline steps first, then the current processor.
        /// This represents chaining steps together in a forward direction.
        /// </summary>
        public override OutputType ExecuteSubPipeline(InputType input, Context context)
        {
            context.Log($"Executing previous pipeline segment(s) (forward flow)...");

            // 1. Execute the pipeline built so far using the initial input.
            //    The previous segment takes InputType and produces IntermediateType.
            var previousPipelineResult = previousPipeline.ExecuteSubPipeline(input, context);

            context.Log($"Executing current processor (forward flow): {currentProcessor.GetType().Name}");

            // 2. Process the result from the previous segment with the current processor.
            //    The current processor takes IntermediateType (output of previous) and produces OutputType.
            var result = currentProcessor.Process(previousPipelineResult, context);

            context.Log($"Current processor (forward flow) finished.");

            // 3. Return the final result of this segment.
            return result;
        }
    }
}
