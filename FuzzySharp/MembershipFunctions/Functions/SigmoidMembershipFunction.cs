namespace FuzzySharp.MembershipFunctions.Functions
{
    public class SigmoidMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private readonly T _crossover;
        private readonly T _slope;

        public SigmoidMembershipFunction(T crossover, T slope)
        {
            _crossover = crossover;
            _slope = slope;
        }

        public SigmoidMembershipFunction(List<T> args) : base(args)
        {
            if (args.Count != 2)
            {
                throw new InvalidDataException($"Invalid number of args provided {args.Count} should 2");
            }

            _crossover = args[1];
            _slope = args[2];
        }

        protected override T CalculateMembership(T x)
        {
            return T.CreateTruncating(1 / (1 + Math.Exp(double.CreateTruncating(-_slope * (x - _crossover)))));
        }

        public override List<T> Introduce()
        {
            return [_crossover, _slope];
        }
    }
}
