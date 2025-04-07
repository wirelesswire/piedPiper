﻿namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationMin<T>: INormOperation<T> where T: INumber<T>
    {
        public T Calculate(T x, T y)
        {
            return x < y ? x : y;
        }
    }
}
