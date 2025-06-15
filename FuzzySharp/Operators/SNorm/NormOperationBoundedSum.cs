namespace FuzzySharp.Operators.SNorm
{
    public class NormOperationBoundedSum<T> : INormOperation<NormOperationBoundedSum<T>, T>
        where T : INumber<T>
    {
        public static T Calculate(T x, T y)
        {
            return T.One < x + y ? T.One : x + y;
        }
    }
}
