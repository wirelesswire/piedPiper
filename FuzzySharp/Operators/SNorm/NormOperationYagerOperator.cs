namespace FuzzySharp.Operators.SNorm
{
    public class NormOperationYagerOperator<T>(double b) : INormOperation<T> where T : INumber<T>
    {
        public T Calculate(T x, T y)
        {
            var part = T.CreateTruncating(
                Math.Pow((Math.Pow(double.CreateTruncating(x), b) + Math.Pow(double.CreateTruncating(y), b)),1 / b));
            return T.One < part ? T.One : part;
        }
    }
}
