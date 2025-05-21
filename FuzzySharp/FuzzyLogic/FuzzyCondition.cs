namespace FuzzySharp.FuzzyLogic
{
    public class FuzzyCondition<T>(string linguisticCondition, Func<T[], T> function, int paramsCount) where T : INumber<T>
    {
        public string GetName()
        {
            return linguisticCondition;
        }

        public T Calculate(T[] x)
        {
            return function(x);
        }

        public int ParamsCount => paramsCount;
    }
}
