namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationAlgebraicProd<T> : INormOperation<T> where T : INumber<T>
    {
        public T Calculate(T x, T y)
        {
            return x * y;
        }
    }
}
