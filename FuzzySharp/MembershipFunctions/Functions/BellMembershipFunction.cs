using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FuzzySharp.MembershipFunctions.Functions
{
    public class BellMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private T _centre;
        private T _width;
        private T _slopes;

        public BellMembershipFunction(T a, T b, T c, T d) : base()
        {
            _centre = c;
            _width = a;
            _slopes = b;
        }
        public BellMembershipFunction(List<T> args) : base(args)
        {
            if (args.Count != 3)
            {
                throw new InvalidDataException($"Invalid number of args provided {args.Count} should 3");
            }

            _centre = args[0];
            _width = args[1];
            _=_slopes = args[2];
        }

        public override T CalculateMembership(T x)
        {
            return T.CreateTruncating(
                1 / 
                    (1 + Math.Pow(
                        Math.Abs(
                            double.CreateTruncating(x - _centre) / 
                            double.CreateTruncating(_slopes)),
                        2*double.CreateTruncating(_slopes))
                    )
            );
        }
    }
}
