namespace FuzzySharp.Operators.SNorm
{
    public class NormOperationYagerOperator<T> : INormOperation<NormOperationYagerOperator<T>, T>
        where T : INumber<T>

    {
        public static T Calculate(T x, T y, double b)
        {
            var part = T.CreateTruncating(
                Math.Pow((Math.Pow(double.CreateTruncating(x), b) + Math.Pow(double.CreateTruncating(y), b)),1 / b));
            return T.One < part ? T.One : part;
        }

        public static T Calculate(T x, T y)
        {
            throw new Exception("Yager operator require usage of override function");
        }
    }
}
