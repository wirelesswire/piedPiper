namespace FuzzySharp.FuzzyLogic
{
    public class FuzzyRule<T> where T : INumber<T>
    {
        private readonly List<FuzzyCondition<T>> _conditions = [];
        private FuzzyCondition<T>? _result;
        private static readonly List<string> _existingRules = [];

        public FuzzyRule(FuzzyCondition<T> initialCondition)
        {
            var condition = initialCondition;
            var conditionName = initialCondition.GetName();

            if (_existingRules.Contains(conditionName))
            {
                condition = new FuzzyCondition<T>(
                    conditionName, 
                    (x) => LinguisticRules<T>.GetInstance().Calculate(conditionName, x), 
                    initialCondition.ParamsCount);
            }

            _conditions.Add(condition);
        }

        public FuzzyRule<T> And(FuzzyCondition<T> condition)
        {
            _conditions.Add(condition);
            return this;
        }

        public FuzzyRule<T> Then(FuzzyCondition<T> result)
        {
            _result = result;
            _existingRules.Add(result.GetName());
            return this;
        }
        public string Name => _result!.GetName();

        public T Calculate(params T[] inputNumbers)
        {
            var conditionsParamsCount = TotalConditionsParamsCount();

            if (inputNumbers.Length != conditionsParamsCount)
                throw new ArgumentException("Input count must match number of conditions.");

            var evaluations = new List<T>();
            var i = 0;
            var firstParamIdx = 0;
            while (i < _conditions.Count)
            {
                var condition = _conditions[i];
                var lastIdx = firstParamIdx + condition.ParamsCount;

                var inputNumbersPart = inputNumbers[firstParamIdx..lastIdx];
                firstParamIdx = lastIdx;

                T result = condition.Calculate(inputNumbersPart);
                evaluations.Add(result);

                i++;
            }

            return _result!.Calculate(evaluations.ToArray());
        }

        private int TotalConditionsParamsCount()
        {
            return _conditions.Sum(cond => cond.ParamsCount);
        }
    }
}
