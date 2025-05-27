namespace FuzzySharp.Operators.SNorm
{
    public class NormOperationEinsteinSum<T> : INormOperation<T> where T : INumber<T>
    {
        public static T Calculate(T x, T y)
        {
            return (x + y) / (T.One + (x * y));
        }
    }
}
