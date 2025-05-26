namespace FuzzySharp.FuzzyLogic
{
    public class FuzzyRuleBuilder<T> where T : INumber<T>
    {
        private FuzzyRule<T> _rule = new();

        public FuzzyRuleBuilder<T> If(FuzzyCondition<T> condition)
        {
            _rule = new FuzzyRule<T>(BuildCondition(condition));
            return this;
        }

        public FuzzyRuleBuilder<T> And(FuzzyCondition<T> condition)
        {
            _rule.AddCondition(BuildCondition(condition));
            return this;
        }

        public FuzzyRule<T> Then(FuzzyCondition<T> result)
        {
            _rule.SetResult(BuildCondition(result));

            var rule = _rule;
            Reset();
            return rule;
        }

        private void Reset()
        {
            LinguisticRules<T>.GetInstance().TryAddRule(_rule);
            _rule = new FuzzyRule<T>();
        }

        private static FuzzyCondition<T> BuildCondition(FuzzyCondition<T> initialCondition)
        {
            var condition = initialCondition;
            var conditionName = initialCondition.GetName();
            var rules = LinguisticRules<T>.GetInstance();

            if (rules.Contains(conditionName))
            {
                condition = new FuzzyCondition<T>(
                    conditionName,
                    (x) => rules.Calculate(conditionName, x),
                    initialCondition.ParamsCount);
            }

            return condition;
        }
    }
}
