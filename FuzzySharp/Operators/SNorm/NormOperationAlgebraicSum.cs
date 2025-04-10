﻿namespace FuzzySharp.Operators.SNorm
{
    public class NormOperationAlgebraicSum<T> : INormOperation<T> where T : INumber<T>
    {
        public T Calculate(T x, T y)
        {
            return (x + y) - (x * y);
        }
    }
}
