namespace FuzzySharp.MembershipFunctions.Functions
{
    public class GaussMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private readonly T _centre;
        private readonly T _width;
        private readonly T _factor;

        public GaussMembershipFunction(T centre, T width, T factor)
        {
            _centre = centre;
            _width = width;
            _factor = factor;
        }
        public GaussMembershipFunction(List<T> args) : base(args)
        {
            if (args.Count != 3)
            {
                throw new InvalidDataException($"Invalid number of args provided {args.Count} should 3");
            }

            _centre = args[0];
            _width = args[1];
            _factor = args[2];
        }

        protected override T CalculateMembership(T x)
        {
            return T.CreateTruncating(Math.Exp(-(1.0f / 2.0f) 
                                               * Math.Pow(
                                                   Math.Abs(
                                                       double.CreateTruncating((x - _centre) / _width)),
                                                   double.CreateTruncating(_factor))));
        }

        public override List<T> Introduce()
        {
            return [_centre, _width, _factor];
        }
    }
}
