namespace FuzzySharp.FuzzyLogic
{
    public class LinguisticRules<T> where T : INumber<T>
    {
        private static LinguisticRules<T>? _instance;
        private static Dictionary<string, FuzzyRule<T>> _rules = new();

        private LinguisticRules(Dictionary<string, FuzzyRule<T>>? rules)
        {
            if (rules is not null)
            {
                if (_rules.Count == 0)
                {
                    _rules = rules;
                }
                else
                {
                    throw new Exception("Attempt to override rules in linguistic rules singleton");
                }
            }
        }

        public static LinguisticRules<T> GetInstance()
        {
            _instance ??= new LinguisticRules<T>(null);
            return _instance;
        }

        public bool TryAddRule(FuzzyRule<T> rule)
        {
            return _rules.TryAdd(rule.Name, rule);
        }

        public T Calculate(string ruleName, params T[] values)
        {
            if(!_rules.TryGetValue(ruleName, out FuzzyRule<T>? rule))
            {
                throw new Exception($"Not defined rule: {ruleName}");
            }

            return rule.Calculate(values);
        }

        public bool Contains(string ruleName) => _rules.ContainsKey(ruleName);
    }
}
