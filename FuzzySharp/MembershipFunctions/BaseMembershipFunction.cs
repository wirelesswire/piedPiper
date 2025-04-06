using System.Numerics;

namespace FuzzySharp.MembershipFunctions
{
    public abstract class BaseMembershipFunction<T> : IMembershipFunction<T> where T : INumber<T>
    {
        public BaseMembershipFunction() { }

        public BaseMembershipFunction(List<T> value) { }

        public abstract T CalculateMembership(T x);

        protected bool OutOfBorders(T x, T bottom, T top) => x < bottom || x > top;

        //tylko propozycja na zrobienie wspólnego konstruktora dla kazdej funkcji przynaleznosci, to co proponowales w StepMembershipFunction
        protected void ValidateArgs<I>(List<T> args, int count) where I : INumber<T>
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
