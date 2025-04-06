using System.Numerics;
using System.Runtime.CompilerServices;

namespace FuzzySharp.MembershipFunctions.Functions
{
    public class GaussMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private T _centre;
        private T _width;
        private T _fuzzificationFactor;

        public GaussMembershipFunction(T a, T b, T c) : base()
        {
            _centre = a;
            _width = b;
            _fuzzificationFactor = c;
        }
        public GaussMembershipFunction(List<T> args) : base(args)
        {
            if (args.Count != 3)
            {
                throw new InvalidDataException($"Invalid number of args provided {args.Count} should 3");
            }

            _centre = args[0];
            _width = args[1];
            _fuzzificationFactor = args[2];
        }

        public override T CalculateMembership(T x)
        {
            T value = ((x - _centre) / _width);

            return T.CreateTruncating(
                Math.Exp(-(1 / 2) 
                * Math.Pow(
                    Math.Abs(
                        double.CreateTruncating(value)), 
                    double.CreateTruncating(_fuzzificationFactor)))
                );
            //https://www.youtube.com/watch?v=6NXnxTNIWkc 1:24
        }
    }
}
