namespace FuzzySharp.Operators.TNorm
{
    public class NormOperationYagerOperatorComplement<T> (double b) : INormOperation<T> where T : INumber<T>
    {
        public T Calculate(T x, T y)
        {
            var part = T.CreateTruncating
                ((Math.Pow(double.CreateTruncating(T.One - x),b)) 
                + 
                (Math.Pow(double.CreateTruncating(1 - Math.Pow(double.CreateTruncating(y),b)),1/b)));
            var min = T.One < part ? T.One : part;
            return T.One - min;
        }

        static T INormOperation<T>.Calculate(T x, T y)
        {
            throw new Exception("Yager operator should not be used as static member.");
        }
    }
}
