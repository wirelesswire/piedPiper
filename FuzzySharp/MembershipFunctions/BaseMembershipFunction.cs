namespace FuzzySharp.MembershipFunctions
{
    public abstract class BaseMembershipFunction<T> : IMembershipFunction<T> where T : INumber<T>
    {
        protected BaseMembershipFunction() { }

        protected BaseMembershipFunction(List<T> value) { }

        public abstract T CalculateMembership(T x);

        public abstract List<T> Introduce();

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
