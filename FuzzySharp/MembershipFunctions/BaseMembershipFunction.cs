namespace FuzzySharp.MembershipFunctions
{
    public abstract class BaseMembershipFunction<T> : IMembershipFunction<T> where T : INumber<T>
    {
        protected BaseMembershipFunction() { }

        protected BaseMembershipFunction(List<T> value) { }

        public abstract List<T> Introduce();

        public T GetMembership(T x) 
        {
            T membershipValue = CalculateMembership(x); 

            if (membershipValue < T.Zero || membershipValue > T.One)
            {
                throw new InvalidDataException($"Membership function must return a value between 0.0 and 1.0 (inclusive). Got {membershipValue}");
            }

            return membershipValue; 
        }

        public T Fuzzyficate(T x, double power)
        {
            return T.CreateTruncating(double.Pow(double.CreateTruncating(x),power));
        }

        public T Defuzzyficate(T x, int root)
        {
            return T.CreateTruncating(double.RootN(double.CreateTruncating(x), root));
        }

        public T Fuzzyficate(T x)
        {
            return x * x;
        }

        public T Defuzzyficate(T x)
        {
            return T.CreateTruncating(Math.Sqrt(double.CreateTruncating(x)));
        }

        public T GetComplement(T x)
        {
            return T.One - GetMembership(x);
        }

        protected abstract T CalculateMembership(T x);

        protected bool OutOfBorders(T x, T bottom, T top) => x < bottom || x > top;

        protected void ValidateArgs<I>(List<T> args, int count) where I : INumber<I>
        {
            if (args.Count != count) 
            {
                throw new InvalidDataException($"Invalid number of args provided {args.Count} should {count}");
            }

            var last = args[0];

            foreach (var next in args.Skip(1).Select((value, i) => new { i, value }))
            {
                if (next.value < last)
                {
                    throw new InvalidDataException($"Args should be in ascending order. Invalid at index {next.i}");
                }
            }
        }
    }
}
