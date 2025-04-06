namespace FuzzySharp.MembershipFunctions
{
    public interface IMembershipFunction<T> where T : INumber<T>
    {
        public T CalculateMembership(T x);

        public List<T> Introduce();
    }
}
