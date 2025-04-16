namespace FuzzySharp.MembershipFunctions.Functions
{
    public class GammaMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private readonly T _bottomBorder;
        private readonly T _topBorder;

        public GammaMembershipFunction(T bottomBorder, T topBorder)
        {
            if (bottomBorder > topBorder)
            {
                throw new InvalidDataException($"Values should satisfy bottomBorder <= topBorder: {bottomBorder} < {topBorder}");
            }

            _bottomBorder = bottomBorder;
            _topBorder = topBorder;
        }

        public GammaMembershipFunction(List<T> args) : base(args)
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

            return (x - _bottomBorder) / (_topBorder - x);
        }

        public override List<T> Introduce()
        {
            return [_bottomBorder, _topBorder];
        }
    }
}