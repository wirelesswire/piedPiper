namespace FuzzySharp.Operators.SNorm
{
    public class NormOperationAlgebraicSum<T> : INormOperation<NormOperationAlgebraicSum<T>, T>
        where T : INumber<T>
    {
        public static T Calculate(T x, T y)
        {
            return (x + y) - (x * y);
        }
    }
}
