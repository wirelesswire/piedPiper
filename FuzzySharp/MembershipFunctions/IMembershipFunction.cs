namespace FuzzySharp.MembershipFunctions
{
    public interface IMembershipFunction<T> where T : INumber<T>
    {
        public T GetMembership(T x);

        public List<T> Introduce();

        public T Fuzzyficate(T x);

        public T Defuzzyficate(T x);

        public T GetComplement(T x);
    }
}
