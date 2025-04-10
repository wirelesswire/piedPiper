namespace FuzzySharp.MembershipFunctions.Functions
{
    public class LeftRightMembershipFunction<T>(
        T centre, 
        T alpha, 
        T beta, 
        Func<T,T> leftFunc, 
        Func<T,T> rightFunc) : BaseMembershipFunction<T> where T : INumber<T>
    {
        protected override T CalculateMembership(T x)
        {
            return x < centre ? leftFunc(x / alpha) : rightFunc(x / beta);
        }

        public override List<T> Introduce()
        {
            throw new Exception("You shouldn't introduce Left-Right membership function due to it complexity");
        }
    }
}
