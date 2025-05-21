namespace FuzzySharp.MembershipFunctions.Functions
{
    public class ClassSMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private readonly T _bottomBorder;
        private readonly T _topBorder;

        public ClassSMembershipFunction(T bottomBorder, T topBorder)
        {
            if (bottomBorder > topBorder)
            {
                throw new InvalidDataException($"Values should satisfy bottomBorder <= topBorder: {bottomBorder} < {topBorder}");
            }

            _bottomBorder = bottomBorder;
            _topBorder = topBorder;
        }

        public ClassSMembershipFunction(List<T> args) : base(args)
        {
            ValidateArgs<T>(args, 3);

            _bottomBorder = args[0];
            _topBorder = args[1];
        }

        protected override T CalculateMembership(T x)
        {
            if (x < _bottomBorder)
            {
                return T.Zero;
            }

            if (x >= _topBorder)
            {
                return T.One;
            }

            var two = (T.One + T.One);

            if (_bottomBorder < x && x <= (_bottomBorder + _topBorder) / two)
            {
                return two * T.CreateTruncating(Math.Pow(double.CreateTruncating((x - _bottomBorder)/(_topBorder - _bottomBorder)), 2));
            }

            return T.One - two * T.CreateTruncating(Math.Pow(double.CreateTruncating((x - _topBorder)/(_topBorder - _bottomBorder)), 2));
        }

        public override List<T> Introduce()
        {
            return [_bottomBorder, _topBorder];
        }
    }
}