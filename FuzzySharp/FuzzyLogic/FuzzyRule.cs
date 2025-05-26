namespace FuzzySharp.FuzzyLogic
{
    public class FuzzyRule<T> where T : INumber<T>
    {
        private readonly List<FuzzyCondition<T>> _conditions = [];
        private FuzzyCondition<T>? _result;

        public FuzzyRule()
        {
        }

        public FuzzyRule(FuzzyCondition<T> condition) 
        {
            AddCondition(condition);
        }

        public void AddCondition(FuzzyCondition<T> condition)
        {
            _conditions.Add(condition);
        }

        public void SetResult(FuzzyCondition<T> result)
        {
            _result = result;
        }

        public string Name => _result!.GetName();

        public T Calculate(params T[] inputNumbers)
        {
            var conditionsParamsCount = TotalConditionsParamsCount();

            if (inputNumbers.Length != conditionsParamsCount)
                throw new ArgumentException("Input count must match number of conditions.");

            return _result!.Calculate(Evaluations(inputNumbers));
        }

        private int TotalConditionsParamsCount()
        {
            return _conditions.Sum(cond => cond.ParamsCount);
        }

        private T[] Evaluations(params T[] inputNumbers)
        {
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

            return evaluations.ToArray();
        }
    }
}
