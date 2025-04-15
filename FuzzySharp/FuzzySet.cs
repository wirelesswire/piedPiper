namespace FuzzySharp
{
    public class FuzzySet<T>(Dictionary<T, T> values) where T : INumber<T>
    {
        public Dictionary<T, T> Values = values;

        public bool IsEmpty() => Values.All(x => x.Value == T.Zero);

        /*TODO
         * TODO: to samo co nizej
         * Zbiór rozmyty A zawiera się w zbiorze rozmytym B,
         * co zapisujemy A ⊂ B, wtedy i tylko wtedy,
         * gdy μA(x) ≤ μB (x) dla każdego x ∈ X.
         */

        public bool ContainsIn(FuzzySet<T> set) => Values.All(x => set.Values.TryGetValue(x.Key, out var compareValue) && compareValue > x.Value);

        /*
         * TODO: Mozemy to tak interpretowac? jak myslisz? zbiory musza miec te same elementy w tym przypadku (inna opcja to wyrzucenie tej operacji gdzies ponad fuzzy set)
         * Zbiór rozmyty A jest równy zbiorowi rozmytemu B,
           * co zapisujemy A = B, wtedy i tylko wtedy,
           * gdy μA(x) = μB (x) dla każdego x ∈ X.
         */
        public bool IsEqualTo(FuzzySet<T> set) => Values.All(x => set.Values.TryGetValue(x.Key, out var compareValue) && compareValue == x.Value);

        public Dictionary<T, T> GetSupport() => Values.Where(x => x.Value > T.Zero).ToDictionary();

        public T? GetHeight() => Values.Max(x => x.Value);

        public Dictionary<T, T> CrossSection(T alpha) => Values.Where(x => x.Value > alpha).ToDictionary();
    }
}
