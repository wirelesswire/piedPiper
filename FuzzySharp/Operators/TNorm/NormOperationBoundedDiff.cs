namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationBoundedDiff<T> : INormOperation<NormOperationBoundedDiff<T>, T>
        where T : INumber<T>
    {
        public static T Calculate(T x, T y)
        {
            return T.Zero > x + y - T.One ? T.Zero : x + y - T.One;
        }
    }
}
