namespace FuzzySharp.MembershipFunctions
{
    public abstract class BaseMembershipFunction<T> : IMembershipFunction<T> where T : INumber<T>
    {
        protected BaseMembershipFunction() { }

        protected BaseMembershipFunction(List<T> value) { }

        private double fuzzy_Level = 1;

        public abstract List<T> Introduce();

        public T GetMembership(T x)
        {
            T membershipValue = CalculateMembership(x);

            checkCorrectMembership(membershipValue);

            return calculateSharpenedValue(membershipValue);
        }
        private void checkCorrectMembership(T x)
        {
            if (x < T.Zero || x > T.One)
            {
                throw new InvalidDataException($"Membership function must return a value between 0.0 and 1.0 (inclusive). Got {x}");
            }
        }
        private T calculateSharpenedValue(T x)
        {
            if (fuzzy_Level == 1)
            {
                return x;
            }
            else
            {
                double z = Math.Pow(Convert.ToDouble(x), fuzzy_Level);
                return T.CreateChecked(z); // Fix: Use T.CreateChecked to convert double to T  
            }
        }
        public void sharpen(double factor)
        {
            fuzzy_Level *= factor;
        }
        public void fuzzyfi(double factor )
        {
            fuzzy_Level /= factor;
        }
        public void setFuzzyLevel(double factor)
        {
            fuzzy_Level = factor;
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
