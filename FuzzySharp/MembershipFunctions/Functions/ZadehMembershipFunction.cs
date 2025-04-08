namespace FuzzySharp.MembershipFunctions.Functions
{
    public class ZadehMembershipFunction<T> : BaseMembershipFunction<T> where T : INumber<T>
    {
        private readonly T _crossPoint;

        public ZadehMembershipFunction(T crossPoint)
        {
            _crossPoint = crossPoint;
        }

        public ZadehMembershipFunction(List<T> args) : base(args)
        {
            ValidateArgs<T>(args, 1);

            _crossPoint = args[0];
        }

        public override T CalculateMembership(T x)
        {
            return T.One / (T.One + (x - _crossPoint) * (x - _crossPoint));
        }

        public override List<T> Introduce()
        {
            return [_crossPoint];
        }
    }
}
