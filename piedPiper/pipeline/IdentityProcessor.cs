using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PipelineSystem;

namespace piedPiper.pipeline
{
    // --- Inside PipelineSystem Class ---

    namespace piedPiper.pipeline
    {
        /// <summary>
        /// A simple processor that passes its input directly as its output.
        /// Used internally for building fluent pipeline branches.
        /// </summary>
        public class IdentityProcessor<T> : IProcessor<T, T>
        {
            public T Process(T input, Context context)
            {
                context.Log($"IdentityProcessor<{typeof(T).Name}>: Passing input through.");
                return input;
            }

        }
    }
}
