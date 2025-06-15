namespace FuzzySharp.Operators.SNorm
{
    public class NormOperationHamacherSum<T> : INormOperation<NormOperationHamacherSum<T>, T>
        where T : INumber<T>
    {
        public static T Calculate(T x, T y)
        {
            return (x + y - ((T.One + T.One) * x * y)) / (T.One - (x * y));
        }
    }
}
