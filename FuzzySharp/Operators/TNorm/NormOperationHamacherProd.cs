namespace FuzzySharp.Operators.TNorm
{
    internal class NormOperationHamacherProd<T> : INormOperation<T> where T : INumber<T>
    {
        public T Calculate(T x, T y)
        {
            return (x * y) / (x + y - (x * y));
        }
    }
}
