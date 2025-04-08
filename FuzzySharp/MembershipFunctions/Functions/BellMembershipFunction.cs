namespace FuzzySharp.MembershipFunctions.Functions
{
    public class BellMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private readonly T _centre;
        private readonly T _width;
        private readonly T _slopes;

        public BellMembershipFunction(T width, T slopes, T centre)
        {
            _centre = centre;
            _width = width;
            _slopes = slopes;
        }

        public BellMembershipFunction(List<T> args) : base(args)
        {
            if (args.Count != 3)
            {
                throw new InvalidDataException($"Invalid number of args provided {args.Count} should 3");
            }

            _centre = args[0];
            _width = args[1];
            _slopes = args[2];
        }

        public override T CalculateMembership(T x)
        {
            return T.CreateTruncating(
                1 / 
                    (1 + Math.Pow(
                        Math.Abs(double.CreateTruncating((x - _centre) / _width)),
                        2*double.CreateTruncating(_slopes))
                    )
            );
        }

        public override List<T> Introduce()
        {
            return [_centre, _width, _slopes];
        }
    }
}
