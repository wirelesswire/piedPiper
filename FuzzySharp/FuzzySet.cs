namespace FuzzySharp
{
    public class FuzzySet<T>(List<T> values) where T : INumber<T>
    {
        public bool IsEmpty() => values.All(x => x == T.Zero);

        /*TODO
         *
         * Zbiór rozmyty A zawiera się w zbiorze rozmytym B,
         * co zapisujemy A ⊂ B, wtedy i tylko wtedy,
         * gdy μA(x) ≤ μB (x) dla każdego x ∈ X.
         *
         *
         * Zbiór rozmyty A jest równy zbiorowi rozmytemu B,
         * co zapisujemy A = B, wtedy i tylko wtedy,
         * gdy μA(x) = μB (x) dla każdego x ∈ X.
         */

        public bool Contains(FuzzySet<T> set) => false;

        public bool IsEqual(FuzzySet<T> set) => false;
    }
}
