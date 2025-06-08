using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PipelineSystem;

namespace piedPiper.pipeline
{


    public static class PipelineExtensions
    {
        public record struct PipelineExecutionResult<InputType, OutputType>(
            InputType Input,
            OutputType Output,
            Context Context,
            Exception Exception = null
        );

        /// <summary>
        /// Executes the pipeline in parallel for each item in the input collection using Parallel.ForEach.
        /// </summary>
        /// <typeparam name="InputType">The pipeline input type.</typeparam>
        /// <typeparam name="OutputType">The pipeline output type.</typeparam>
        /// <param name="pipeline">The pipeline to execute.</param>
        /// <param name="inputs">The collection of inputs to process.</param>
        /// <param name="maxDegreeOfParallelism">Optional: Limits the number of concurrent operations. -1 means no limit (default).</param>
        /// <returns>An enumerable of results, one for each input item, including output, context, and any exceptions.</returns>
        public static IEnumerable<PipelineExecutionResult<InputType, OutputType>> ExecuteBatchParallel<InputType, OutputType>(
            this IPipeline<InputType, OutputType> pipeline,
            IEnumerable<InputType> inputs,
            int? maxDegreeOfParallelism = null)
        {
            var results = new ConcurrentBag<PipelineExecutionResult<InputType, OutputType>>();
            ;

            Parallel.ForEach(inputs, new ParallelOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism != null ? maxDegreeOfParallelism.Value : -1
            }, input =>
            {
                Context context = null;
                OutputType output = default;
                Exception caughtException = null;

                try
                {
                    // Execute the entire pipeline for the current input item.
                    // A new context is created internally by Execute.
                    output = pipeline.Execute(input, out context);
                }
                catch (Exception ex)
                {
                    caughtException = ex;
                    // If Execute throws before context is assigned, create a basic one for logging
                    if (context == null)
                    {
                        context = new Context();
                        context.Log($"Execution failed for input (context creation might have failed or happened before exception): {ex.Message}");
                        context.ProcessEndedAt = DateTime.Now; // Mark end time even on failure
                    }
                    else
                    {
                    }
                }

                results.Add(new PipelineExecutionResult<InputType, OutputType>(
                    Input: input,
                    Output: output,
                    Context: context,
                    Exception: caughtException
                ));
            });

            return results;
        }
    }





}
