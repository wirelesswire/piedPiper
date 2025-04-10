namespace FuzzySharp.MembershipFunctions.Functions
{
    public class StepMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private readonly List<T> _steps;

        public StepMembershipFunction(List<T> args) : base(args)
        {
            ValidateArgs<T>(args, args.Count);
            args.Reverse();
            _steps = args;
        }

        protected override T CalculateMembership(T x)
        {
            foreach (var step in _steps.Select((value, i) => new { i, value }))
            {
                if (x >= step.value)
                {
                    return T.CreateTruncating((_steps.Count - step.i) * (1 / _steps.Count));
                }
            }

            return T.Zero;
        }

        public override List<T> Introduce()
        {
            return _steps;
        }
    }
}
