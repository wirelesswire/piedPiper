namespace FuzzySharp
{
    public class FuzzyRelation<T> where T : INumber<T>
    {
        private readonly Dictionary<(T, T), (T, T)> _cartesianProduct;

        private readonly Func<T, T, T> _calcFunc;


        //TODO: fix it dont review this class
        public FuzzyRelation(FuzzySet<T> fsA, FuzzySet<T> fsB, Func<T, T, T> calcFunc)
        {
            _cartesianProduct = fsA.Values.SelectMany(
                a => fsB.Values,
                (a, b) => new KeyValuePair<(T, T), (T, T)>((a.Key, b.Key), (a.Value, b.Value))
            ).ToDictionary(p => p.Key, p => p.Value);

            _calcFunc = calcFunc;
        }

        public T CalculateRelation(T x, T y)
        {
            var relation = _calcFunc(x, y);

            if (relation < T.Zero || relation > T.One)
            {
                throw new InvalidDataException($"Relation function must return a value between 0.0 and 1.0 (inclusive). Got {relation}");
            }

            return relation;
        }
    }
}
