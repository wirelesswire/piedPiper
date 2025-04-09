namespace FuzzySharp.Operators.SNorm
{
    public class NormOperationBoundedSum<T> : INormOperation<T> where T : INumber<T>
    {
        public T Calculate(T x, T y)
        {
            return T.One < x + y ? T.One : x + y;
        }
    }
}
