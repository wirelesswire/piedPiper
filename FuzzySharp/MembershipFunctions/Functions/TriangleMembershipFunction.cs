using System;
using System.Numerics;

namespace FuzzySharp.MembershipFunctions.Functions
{
    public class TriangleMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private T _bottomBorder;
        private T _topBorder;
        private T _trianglePeak;

        public TriangleMembershipFunction(T a, T b, T c) : base()
        {
            if (a > b || b > c)
            {
                throw new InvalidDataException($"Values should satisfy a <= b <= c: {a} < {c} < {b}");
            }

            _bottomBorder = a;
            _trianglePeak = b;
            _topBorder = c;
        }

        public TriangleMembershipFunction(List<T> args) : base(args)
        {
            ValidateArgs<T>(args, 3);

            _bottomBorder = args[0];
            _topBorder = args[1];
            _trianglePeak = args[2];
        }

        public override T CalculateMembership(T x)
        {
            if (OutOfBorders(x, _bottomBorder, _topBorder))
            {
                return T.Zero;
            }

            if (x >= _bottomBorder && x <= _trianglePeak)
            {
                return (x - _bottomBorder) / (_trianglePeak - _bottomBorder);
            }

            return (_topBorder - x) / (_topBorder - _trianglePeak);
        }
    }
}