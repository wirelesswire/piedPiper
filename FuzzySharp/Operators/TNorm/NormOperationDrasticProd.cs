namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationDrasticProd<T> : INormOperation<NormOperationDrasticProd<T>, T>
        where T : INumber<T>
    {
        public static T Calculate(T x, T y)
        {
            return x == T.One || y == T.One ? (x < y ? x : y) : T.Zero;
        }
    }
}
