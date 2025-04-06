using System.Numerics;

namespace FuzzySharp.MembershipFunctions.Functions
{
    public class TrapezeMembersipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private T _bottomBorder;
        private T _topBorder;
        private T _leftPeak;
        private T _rightPeak;

        public TrapezeMembersipFunction(T a, T b, T c, T d) : base()
        {
            if (a > b || b > c || c > d)
            {
                throw new InvalidDataException($"Values should satisfy a <= c <= b <= d: {a} < {c} < {b} < {d}");
            }

            _bottomBorder = a;
            _leftPeak = b;
            _rightPeak = c;
            _topBorder = d;
        }

        public TrapezeMembersipFunction(List<T> args) : base(args)
        {
            ValidateArgs<T>(args, 4);

            _bottomBorder = args[0];
            _leftPeak = args[1];
            _rightPeak = args[2];
            _topBorder = args[3];
        }

        public override T CalculateMembership(T x)
        {
            if (OutOfBorders(x, _bottomBorder, _topBorder))
            {
                return T.Zero;
            }

            if (x < _leftPeak) 
            {
                return (x - _bottomBorder) / (_leftPeak - _bottomBorder);
            }

            if (x > _rightPeak) 
            {
                return (_topBorder - x) / (_topBorder - _rightPeak);
            }

            return T.One;
        }
    }
}
