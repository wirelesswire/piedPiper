using FuzzySharp.FuzzyLogic;
using piedPiper.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzySharp;
using FuzzySharp.FuzzyLogic;
using FuzzySharp.MembershipFunctions;
using FuzzySharp.MembershipFunctions.Functions;
using FuzzySharp.Operators.TNorm; // Do łączenia warunków (np. AND = Min)
using piedPiper.implementacje.Hipki;
using FuzzySharp.Operators.SNorm; // Aby móc korzystać z klas Hipek i ocenionyHipek

namespace piedPiper.implementacje.Hipki
{


        public class HipekValidatorProcessor : IProcessor<Hipek, Hipek>
        {
            public Hipek Process(Hipek input, Context context)
            {
                if (input == null) throw new ArgumentNullException(nameof(input));
                context.Log($"Waliduję Hipeka: {input.name} (Wzrost: {input.height}cm)");

                if (input.height < 150 || input.height > 220)
                {
                    context.Log($"Ostrzeżenie: Hipek {input.name} ma nietypowy wzrost ({input.height}cm).");
                    // Można by tu rzucić wyjątek, ale dla demo pozwolimy na kontynuację
                }
                return input;
            }
        }

        // 2. Normalizacja Danych
        public class HipekNormalizerProcessor : IProcessor<Hipek, Hipek>
        {
            public Hipek Process(Hipek input, Context context)
            {
                if (input == null) throw new ArgumentNullException(nameof(input));
                context.Log($"Normalizuję dane dla Hipeka: {input.name}");

                // Tworzymy kopię Hipeka, aby nie modyfikować oryginału, jeśli byłby używany gdzie indziej.
                // W tym prostym przypadku, po prostu modyfikujemy input, ale w bardziej złożonym potoku
                // warto rozważyć tworzenie nowych instancji.
                input.hairColor = input.hairColor?.ToLowerInvariant();
                input.eyeColor = input.eyeColor?.ToLowerInvariant();

                context.Log($"Znormalizowany Hipek: Włosy: {input.hairColor}, Oczy: {input.eyeColor}");
                return input;
            }
        }

        // 3. Ocena Rozmyta (serce integracji FuzzySharp)
        public class HipekFuzzyEvaluatorProcessor : IProcessor<Hipek, ocenionyHipek>
        {
            private readonly LinguisticRules<float> rules;

            public HipekFuzzyEvaluatorProcessor()
            {
                // Konfiguracja logiki rozmytej w konstruktorze, aby była jednokrotna
                var tallCondition = new FuzzyCondition<float>("tall", (height) =>
                {
                    // Funkcja przynależności dla "wysoki" (np. od 175cm do 190cm i więcej)
                    return new SigmoidMembershipFunction<float>(180f, 0.1f).GetMembership(height.FirstOrDefault());
                }, 1);

                var veryTallCondition = new FuzzyCondition<float>("veryTall", (height) =>
                {
                    // Funkcja przynależności dla "bardzo wysoki"
                    return new SigmoidMembershipFunction<float>(188f, 0.15f).GetMembership(height.FirstOrDefault());
                }, 1);

                var hasGoodHairCondition = new FuzzyCondition<float>("goodHair", (hairColorScore) =>
                {
                    // Funkcja przynależności dla "ładne włosy" (np. blonde, brown są bardziej "ładne" niż black/red w tym przykładzie)
                    // Zakładamy, że hairColorScore będzie wartością zmapowaną z koloru włosów
                    return new TriangleMembershipFunction<float>(0.3f, 0.6f, 0.9f).GetMembership(hairColorScore.FirstOrDefault());
                }, 1);

                // Reguła oceniająca "dobry Hipek"
                // If (wysoki AND ma ładne włosy) OR bardzo wysoki THEN dobraOcena
                var goodHipekRule = new FuzzyCondition<float>("goodHipek", (inputs) =>
                {
                    float isTall = inputs[0]; // z tallCondition
                    float hasGoodHair = inputs[1]; // z goodHairCondition
                    //float isVeryTall = inputs[2]; // z veryTallCondition

                    // Warunek: wysoki ORAZ ładne włosy (używamy T-normy MIN dla AND)
                    float tallAndGoodHair = NormOperationMax<float>.Calculate(isTall, hasGoodHair);

                    return tallAndGoodHair;
                    // Warunek: (wysoki AND ładne włosy) LUB bardzo wysoki (używamy S-normy MAX dla OR)
                    //return NormOperationMax<float>.Calculate(tallAndGoodHair);
                }, 3); // Oczekuje 3 wejść (tall, goodHair, veryTall)

                var fuzzyRuleBuilder = new FuzzyRuleBuilder<float>();

                fuzzyRuleBuilder.If(tallCondition)
                    .And(hasGoodHairCondition)
                    .Then(goodHipekRule); 
                rules = LinguisticRules<float>.GetInstance();
            }

            public ocenionyHipek Process(Hipek input, Context context)
            {
                if (input == null) throw new ArgumentNullException(nameof(input));
                context.Log($"Rozpoczynam fuzzy ocenę Hipek: {input.name}");

                // Mapowanie koloru włosów na wartość numeryczną dla funkcji przynależności
                float hairColorScore = MapHairColorToFuzzyScore(input.hairColor);

                // Obliczenie oceny rozmytej
                // Przekazujemy wartości dla 'tallCondition', 'goodHairCondition'
                float finalScore = rules.Calculate("goodHipek", input.height, hairColorScore);
                // Ważne: Calculate przyjmuje wartości wejściowe w kolejności odpowiadającej inputom w funkcji goodHipekRule (inputs[0], inputs[1], inputs[2])
                // Czyli: height (dla tallCondition), hairColorScore (dla goodHairCondition), height (dla veryTallCondition)

                context.Log($"Fuzzy ocena dla {input.name} (wzrost: {input.height}, włosy: {input.hairColor}): {finalScore:F2}");
                return new ocenionyHipek(input, finalScore);
            }

            // Pomocnicza metoda do mapowania koloru włosów na wartość numeryczną
            private float MapHairColorToFuzzyScore(string hairColor)
            {
                return hairColor switch
                {
                    "blonde" => 0.9f,
                    "brown" => 0.7f,
                    "black" => 0.4f,
                    "red" => 0.6f,
                    _ => 0.1f // Domyślna niska wartość dla nieznanych kolorów
                };
            }
        }

        // 4. Logowanie Wyniku
        public class EvaluatedHipekLoggerProcessor : IProcessor<ocenionyHipek, ocenionyHipek>
        {
            public ocenionyHipek Process(ocenionyHipek input, Context context)
            {
                if (input == null) throw new ArgumentNullException(nameof(input));
                context.Log($"Logger: Hipek {input.hipek.name} otrzymał ocenę: {input.ocena:F2}");
                return input;
            }
        }

        // 5. Kategoryzacja Oceny
        public class ScoreCategorizerProcessor : IProcessor<ocenionyHipek, string>
        {
            public string Process(ocenionyHipek input, Context context)
            {
                if (input == null) throw new ArgumentNullException(nameof(input));
                string category;
                if (input.ocena >= 0.8)
                {
                    category = "Świetny";
                }
                else if (input.ocena >= 0.6)
                {
                    category = "Bardzo Dobry";
                }
                else if (input.ocena >= 0.4)
                {
                    category = "Przeciętny";
                }
                else
                {
                    category = "Słaby";
                }
                context.Log($"Kategoryzacja oceny dla {input.hipek.name}: {input.ocena:F2} -> {category}");
                return category;
            }
        }

        // 6. Budowa Wiadomości Końcowej
        public class FinalMessageBuilderProcessor : IProcessor<string, string>
        {
            public string Process(string input, Context context)
            {
                if (input == null) throw new ArgumentNullException(nameof(input));
                context.Log($"Buduję finalną wiadomość.");
                return $"Finalna ocena: {input}.";
            }
        }
    }


