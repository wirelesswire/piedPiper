namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationEinsteinProd<T> : INormOperation<T> where T : INumber<T>
    {
        public T Calculate(T x, T y)
        {
            return (x * y) / ( (T.One + T.One) - (x + y - (x * y)));
        }
    }
}
