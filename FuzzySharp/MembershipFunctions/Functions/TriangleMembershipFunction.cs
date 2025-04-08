namespace FuzzySharp.MembershipFunctions.Functions
{
    public class TriangleMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private readonly T _bottomBorder;
        private readonly T _topBorder;
        private readonly T _trianglePeak;

        public TriangleMembershipFunction(T bottomBorder, T trianglePeak, T topBorder)
        {
            if (bottomBorder > trianglePeak || trianglePeak > topBorder)
            {
                throw new InvalidDataException($"Values should satisfy bottomBorder <= trianglePeak <= topBorder: {bottomBorder} < {topBorder} < {trianglePeak}");
            }

            _bottomBorder = bottomBorder;
            _trianglePeak = trianglePeak;
            _topBorder = topBorder;
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

        public override List<T> Introduce()
        {
            return [_bottomBorder, _trianglePeak, _topBorder];
        }
    }
}