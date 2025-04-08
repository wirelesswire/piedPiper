namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationDrasticProd<T> : INormOperation<T> where T : INumber<T>
    {
        //TODO: nie znormalizowane tu sie sypna bo x albo y moze byc wieksze od 1, POTRZEBNA NORMALIZACJA!
        public T Calculate(T x, T y)
        {
            return x == T.One || y == T.One ? (x < y ? x : y) : T.Zero;
        }
    }
}
