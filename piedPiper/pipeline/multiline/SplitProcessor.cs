using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piedPiper.pipeline
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    // --- Inside PipelineSystem Class ---

    namespace piedPiper.pipeline
    {
        /// <summary>
        /// An IProcessor that takes a single input, executes multiple child pipelines in parallel,
        /// and returns an IEnumerable of their results.
        /// </summary>
        /// <typeparam name="TInput">The input type for this SplitProcessor (and for each branch pipeline).</typeparam>
        /// <typeparam name="TBranchOutput">The common output type for all parallel branches.</typeparam>
        public class SplitProcessor<TInput, TBranchOutput> : IProcessor<TInput, IEnumerable<TBranchOutput>>
        {
            private readonly IEnumerable<IPipeline<TInput, TBranchOutput>> _branches;

            public SplitProcessor(IEnumerable<IPipeline<TInput, TBranchOutput>> branches)
            {
                _branches = branches ?? throw new ArgumentNullException(nameof(branches));
                if (!_branches.Any()) throw new ArgumentException("Branches collection cannot be empty.", nameof(branches));
            }

            public IEnumerable<TBranchOutput> Process(TInput input, Context context)
            {
                context.Log($"SplitProcessor starting for input type {typeof(TInput).Name} with {_branches.Count()} branches.");

                // Store results including any exceptions for later aggregation.
                var branchExecutionResults = new ConcurrentBag<PipelineExtensions.PipelineExecutionResult<TInput, TBranchOutput>>();
                var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = -1 }; // Or configurable if needed

                Parallel.ForEach(_branches, (branchPipeline, loopState, index) =>
                {
                    // Each branch pipeline gets its own context to track its execution independently.
                    // The branchPipeline.Execute method handles creating and populating this context.
                    Context branchContext = null;
                    TBranchOutput branchOutput = default;
                    Exception branchException = null;

                    try
                    {
                        context.Log($"Branch {index} pipeline starting execution (ID: {branchPipeline.GetType().Name}).");
                        branchOutput = branchPipeline.Execute(input, out branchContext);
                        context.Log($"Branch {index} pipeline finished successfully.");
                    }
                    catch (Exception ex)
                    {
                        branchException = ex;
                        context.Log($"Branch {index} pipeline failed: {ex.Message}");
                        // Ensure a context exists even if the exception happened very early
                        if (branchContext == null)
                        {
                            branchContext = new Context();
                            branchContext.Log($"Branch {index} execution failed before full context initialization: {ex.Message}");
                            branchContext.ProcessEndedAt = DateTime.Now;
                        }
                    }
                    finally
                    {
                        branchExecutionResults.Add(new PipelineExtensions.PipelineExecutionResult<TInput, TBranchOutput>(
                            Input: input,
                            Output: branchOutput,
                            Context: branchContext,
                            Exception: branchException
                        ));
                    }
                });

                context.Log("SplitProcessor finished all branches. Aggregating results.");

                // Aggregate logs from each branch into the main context
                foreach (var result in branchExecutionResults.OrderBy(r => r.Context.ProcessStartedAt)) // Order by start time for readability
                {
                    context.Log($"--- Branch Summary (Token: {result.Context.UniqueToken}, Time: {result.Context.ProcessTimeInMilliseconds} ms) ---");
                    foreach (var log in result.Context.Logs)
                    {
                        context.Log($"  {log}");
                    }
                    if (result.Exception != null)
                    {
                        context.Log($"  Branch ERROR: {result.Exception.Message}");
                    }
                    context.Log($"--- End Branch Summary ---");
                }

                // If any branch failed, rethrow as an AggregateException
                var failedBranches = branchExecutionResults.Where(r => r.Exception != null).ToList();
                if (failedBranches.Any())
                {
                    throw new AggregateException("One or more pipeline branches failed.", failedBranches.Select(r => r.Exception));
                }

                return branchExecutionResults.Select(r => r.Output);
            }
        }
    }
}
