namespace FuzzySharp.Operators
{
    public interface INormOperation<TSelf, T>
        where TSelf : INormOperation<TSelf, T>
    {
        static abstract T Calculate(T a, T b);
    }
}
