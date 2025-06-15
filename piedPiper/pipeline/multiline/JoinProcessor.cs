using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piedPiper.pipeline.multiline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static global::PipelineSystem;

    // --- Inside PipelineSystem Class ---

    public partial class PipelineSystem
    {
        /// <summary>
        /// Abstract base class for processors that aggregate an enumerable of inputs into a single output.
        /// </summary>
        /// <typeparam name="TInputItem">The type of each item in the input enumerable.</typeparam>
        /// <typeparam name="TOutput">The single aggregated output type.</typeparam>
        public abstract class JoinProcessor<TInputItem, TOutput> : IProcessor<IEnumerable<TInputItem>, TOutput>
        {
            public abstract TOutput Process(IEnumerable<TInputItem> inputs, Context context);
        }

        /// <summary>
        /// Joins a collection of integers by summing them.
        /// </summary>
        public class SumIntJoinProcessor : JoinProcessor<int, int>
        {
            public override int Process(IEnumerable<int> inputs, Context context)
            {
                context.Log("SumIntJoinProcessor: Aggregating integer results by summing.");
                if (inputs == null)
                {
                    context.Log("SumIntJoinProcessor: Input enumerable is null, returning 0.");
                    return 0;
                }
                int sum = inputs.Sum();
                context.Log($"SumIntJoinProcessor: Total sum = {sum}.");
                return sum;
            }
        }

        /// <summary>
        /// Joins a collection of strings by concatenating them with a specified separator.
        /// </summary>
        public class CombineStringsJoinProcessor : JoinProcessor<string, string>
        {
            private readonly string _separator;

            public CombineStringsJoinProcessor(string separator = ", ")
            {
                _separator = separator ?? "";
            }

            public override string Process(IEnumerable<string> inputs, Context context)
            {
                context.Log($"CombineStringsJoinProcessor: Combining string results with separator '{_separator}'.");
                if (inputs == null)
                {
                    context.Log("CombineStringsJoinProcessor: Input enumerable is null, returning empty string.");
                    return string.Empty;
                }
                string combined = string.Join(_separator, inputs);
                context.Log($"CombineStringsJoinProcessor: Combined string = '{combined}'.");
                return combined;
            }
        }

        /// <summary>
        /// A generic JoinProcessor that selects the first item from the enumerable.
        /// Useful if you only expect one result or need a default.
        /// </summary>
        public class PickFirstJoinProcessor<T> : JoinProcessor<T, T>
        {
            public override T Process(IEnumerable<T> inputs, Context context)
            {
                context.Log("PickFirstJoinProcessor: Selecting the first item.");
                if (inputs == null || !inputs.Any())
                {
                    context.Log("PickFirstJoinProcessor: Input enumerable is empty or null, returning default.");
                    return default;
                }
                T first = inputs.First();
                context.Log($"PickFirstJoinProcessor: Selected '{first}'.");
                return first;
            }
        }
    }
}
