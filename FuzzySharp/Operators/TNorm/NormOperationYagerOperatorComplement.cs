namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationYagerOperatorComplement<T> : INormOperation<NormOperationYagerOperatorComplement<T>, T>
        where T : INumber<T>
    {
        public T Calculate(T x, T y, double b)
        {
            var part = T.CreateTruncating
                ((Math.Pow(double.CreateTruncating(T.One - x),b)) 
                + 
                (Math.Pow(double.CreateTruncating(1 - Math.Pow(double.CreateTruncating(y),b)),1/b)));
            var min = T.One < part ? T.One : part;
            return T.One - min;
        }

        public static T Calculate(T x, T y)
        {
            throw new Exception("Yager operator require usage of override function");
        }
    }
}
