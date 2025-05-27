namespace FuzzySharp.Operators
{
    public interface INormOperation<T> where T : INumber<T>
    {
        public static abstract T Calculate(T x, T y);
    }
}
