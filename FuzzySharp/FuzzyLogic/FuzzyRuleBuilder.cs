namespace FuzzySharp.FuzzyLogic
{
    public static class FuzzyRuleBuilder<T> where T : INumber<T>
    {
        public static FuzzyRule<T> If(FuzzyCondition<T> condition)
        {
            return new FuzzyRule<T>(condition);
        }
    }
}
