namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationAlgebraicProd<T> : INormOperation<NormOperationAlgebraicProd<T>, T>
        where T : INumber<T>
    {
        public static T Calculate(T x, T y)
        {
            return x * y;
        }
    }
}
