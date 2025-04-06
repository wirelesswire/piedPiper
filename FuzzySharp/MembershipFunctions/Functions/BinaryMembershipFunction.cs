namespace FuzzySharp.MembershipFunctions.Functions
{
    public class BinaryMembershipFunction<T>(T crossPoint, bool inclusive) : BaseMembershipFunction<T> where T : INumber<T>
    {
        public override T CalculateMembership(T x)
        {
            return inclusive ? 
                (x <= crossPoint ? T.One : T.Zero) : 
                (x < crossPoint ? T.One : T.Zero);
        }

        public override List<T> Introduce()
        {
            var inc = inclusive ? T.One : T.Zero;
            return [crossPoint, inc];
        }
    }
}
