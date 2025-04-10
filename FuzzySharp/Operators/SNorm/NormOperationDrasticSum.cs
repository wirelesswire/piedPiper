namespace FuzzySharp.Operators.SNorm
{
    public class NormOperationDrasticSum<T> : INormOperation<T> where T : INumber<T>
    {
        public T Calculate(T x, T y)
        {
            return x == T.Zero || y == T.Zero ? (x > y ? x : y) : T.One;
        }
    }
}
