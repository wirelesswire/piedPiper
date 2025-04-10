namespace FuzzySharp.MembershipFunctions.Functions
{
    public class TrapezeMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private readonly T _bottomBorder;
        private readonly T _topBorder;
        private readonly T _leftPeak;
        private readonly T _rightPeak;

        public TrapezeMembershipFunction(T bottomBorder, T leftPeak, T rightPeak, T topBorder)
        {
            if (bottomBorder > leftPeak || leftPeak > rightPeak || rightPeak > topBorder)
            {
                throw new InvalidDataException($"Values should satisfy bottomBorder <= rightPeak <= leftPeak <= topBorder: {bottomBorder} < {rightPeak} < {leftPeak} < {topBorder}");
            }

            _bottomBorder = bottomBorder;
            _leftPeak = leftPeak;
            _rightPeak = rightPeak;
            _topBorder = topBorder;
        }

        public TrapezeMembershipFunction(List<T> args) : base(args)
        {
            ValidateArgs<T>(args, 4);

            _bottomBorder = args[0];
            _leftPeak = args[1];
            _rightPeak = args[2];
            _topBorder = args[3];
        }

        protected override T CalculateMembership(T x)
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

        public override List<T> Introduce()
        {
            return [_bottomBorder, _leftPeak, _rightPeak, _topBorder];
        }
    }
}
