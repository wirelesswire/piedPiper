namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationEinsteinProd<T> : INormOperation<NormOperationEinsteinProd<T>, T>
        where T : INumber<T>
    {
        public static T Calculate(T x, T y)
        {
            return (x * y) / ( (T.One + T.One) - (x + y - (x * y)));
        }
    }
}
