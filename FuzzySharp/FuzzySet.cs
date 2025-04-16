using System.Xml.XPath;

namespace FuzzySharp
{
    public class FuzzySet<T>(Dictionary<T, T> values) where T : INumber<T>
    {
        public Dictionary<T, T> Values = values;

        public bool IsEmpty() => Values.All(x => x.Value == T.Zero);

        public bool ContainsIn(FuzzySet<T> set) => Values.All(x => set.Values.TryGetValue(x.Key, out var compareValue) && compareValue > x.Value);

        public bool IsEqualTo(FuzzySet<T> set) => Values.All(x => set.Values.TryGetValue(x.Key, out var compareValue) && compareValue == x.Value);

        public Dictionary<T, T> GetSupport() => Values.Where(x => x.Value > T.Zero).ToDictionary();

        public T? GetHeight() => Values.Max(x => x.Value);

        public Dictionary<T, T> CrossSection(T alpha) => Values.Where(x => x.Value > alpha).ToDictionary();

        public FuzzySet<T> GetCompletionSet() => new (Values.ToDictionary(pair => pair.Key, pair => T.One - pair.Value));
    }
}
