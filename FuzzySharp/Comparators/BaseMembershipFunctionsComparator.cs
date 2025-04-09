using FuzzySharp.MembershipFunctions;

namespace FuzzySharp.Comparators
{
    public class BaseMembershipFunctionsComparator
    {
        public static bool Compare<T>(IMembershipFunction<T> first, IMembershipFunction<T> second) where T : INumber<T>
        {
            if (first.GetType() != second.GetType()) return false;

            var list1 = first.Introduce();
            var list2 = second.Introduce();

            var firstNotSecond = list1.Except(list2).ToList();
            var secondNotFirst = list2.Except(list1).ToList();

            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }
    }
}
