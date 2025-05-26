namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationHamacherProd<T> : INormOperation<T> where T : INumber<T>
    {
        public static T Calculate(T x, T y)
        {
            return (x * y) / (x + y - (x * y));
        }
    }
}
