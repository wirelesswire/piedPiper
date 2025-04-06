using System.Numerics;

namespace FuzzySharp.MembershipFunctions
{
    public interface IMembershipFunction<T> where T : INumber<T>
    {
        public abstract T CalculateMembership(T x);
    }
}
