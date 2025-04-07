using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzySharp.Operators
{
    public interface INormOperation<T> where T : INumber<T>
    {
        public T Calculate(T x, T y);
    }
}
