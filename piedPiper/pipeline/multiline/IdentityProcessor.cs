using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace piedPiper.pipeline
{
    public class IdentityProcessor<T> : IProcessor<T, T>
    {
        public T Process(T input, Context context)
        {
            context.Log($"IdentityProcessor<{typeof(T).Name}>: Passing input through.");
            return input;
        }

    }
}
