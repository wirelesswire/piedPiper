using FuzzySharp;
using FuzzySharp.FuzzyLogic;
using FuzzySharp.MembershipFunctions;
using FuzzySharp.MembershipFunctions.Functions;
using FuzzySharp.Operators.SNorm;
using FuzzySharp.Operators.TNorm;

namespace FuzzySharpSamples
{
    public class Program
    {
        static void Main(string[] args)
        {
            BaseMembershipSample();
            BaseRelationSample();
            BaseLogicSample();
        }

        public static int UseSampleClass() 
        {
            return ExampleClass.Add(5, 10);
        }

        public static void BaseMembershipSample()
        {
            float LeftFunc(float x) => float.CreateTruncating(Math.Abs(Math.Cos(x)));

            float RightFunc(float x) => float.CreateTruncating(Math.Abs(Math.Sin(x)));

            var mbFunction1 = new TrapezeMembershipFunction<float>(1, 2, 3, 4);
            var mbFunction2 = new GaussMembershipFunction<float>(2.5f, 2, 3);
            var mbFunction3 = new TriangleMembershipFunction<float>(1, 2.5f, 4);
            var mbFunction4 = new BellMembershipFunction<float>(2, 2, 3);
            var mbFunction5 = new SigmoidMembershipFunction<float>(2.5f, 1);
            var mbFunction6 = new LeftRightMembershipFunction<float>(2.5f, 1, 1, LeftFunc, RightFunc);
            var mbFunction7 = new BinaryMembershipFunction<float>(2.5f, true);
            var mbFunction8 = new ZadehMembershipFunction<float>(2.5f);

            List<IMembershipFunction<float>> list = [mbFunction1, mbFunction2, mbFunction3, mbFunction4, mbFunction5, mbFunction6, mbFunction7, mbFunction8];

            foreach (var mbFunction in list)
            {
                Console.WriteLine($"Membership (1.5): {mbFunction.GetMembership(1.5f)}");
                Console.WriteLine($"Membership (2.5): {mbFunction.GetMembership(2.5f)}");
            }
        }

        public static void BaseRelationSample()
        {
            var dictionary = new Dictionary<float, float>();
            var dictionary2 = new Dictionary<float, float>();

            dictionary.Add(1, 1);
            dictionary.Add(2, 2);
            dictionary2.Add(3, 3);
            dictionary2.Add(4, 4);

            float Func(float arg1, float arg2)
            {
                return Math.Abs((arg1 - arg2) / (arg1 * arg2));
            }

            var fuzzySet1 = new FuzzySet<float>(dictionary);
            var fuzzySet2 = new FuzzySet<float>(dictionary2);
            var fuzzyRelation = new FuzzyRelation<float>(fuzzySet1, fuzzySet2, Func);

            fuzzyRelation.CalculateRelation(1, 3);
            fuzzyRelation.CalculateRelation(1, 4);
        }

        public static void BaseLogicSample()
        {
            var tallCondition = new FuzzyCondition<float>("tallCond", (x) =>
            {
                var function = new SigmoidMembershipFunction<float>(180f, 0.3f);
                return function.GetMembership(x.FirstOrDefault());
            },1);

            var fatCondition = new FuzzyCondition<float>("fatCond", (x) =>
            {
                var function = new SigmoidMembershipFunction<float>(95f, 0.2f);
                return function.GetMembership(x.FirstOrDefault());
            },1);

            var fastCondition = new FuzzyCondition<float>("fastCondition", (x) =>
            {
                var function = new SigmoidMembershipFunction<float>(30f, 0.07f);
                return function.GetMembership(x.FirstOrDefault());
            },1);

            var fastRule = new FuzzyCondition<float>("fastRule", (x) => x[0],1);

            var bigRule = new FuzzyCondition<float>("bigRule", (x) =>
            {
                var operation = new NormOperationMax<float>();
                return operation.Calculate(x[0], x[1]);
            },2);

            var tallRule = new FuzzyCondition<float>("tallRule", (x) => x[0],1);

            var goodRugbyPlayerRule = new FuzzyCondition<float>("goodRugbyPlayerRule", (x) =>
            {
                var operation = new NormOperationMax<float>();
                return operation.Calculate(x[0], x[1]);
            },3);

            var fastFatTallRule = new FuzzyCondition<float>("fastFatTallRule", (x) =>
            {
                var operation = new NormOperationMin<float>();
                var fastAndFat = operation.Calculate(x[0], x[1]);
                return operation.Calculate(fastAndFat, x[2]);
            }, 3);

            var firstRule = FuzzyRuleBuilder<float>.If(tallCondition)
                                                    .And(fatCondition)
                                                    .Then(bigRule);

            var secondRule = FuzzyRuleBuilder<float>.If(tallCondition)
                                                    .Then(tallRule);

            var thirdRule = FuzzyRuleBuilder<float>.If(fastCondition)
                                                    .Then(fastRule);

            var fourthRule = FuzzyRuleBuilder<float>.If(bigRule)
                                                    .And(fastCondition)
                                                    .Then(goodRugbyPlayerRule);

            var fifthRule = FuzzyRuleBuilder<float>.If(fastCondition)
                                                    .And(fatCondition)
                                                    .And(tallCondition)
                                                    .Then(fastFatTallRule);

            var rules = LinguisticRules<float>.GetInstance();

            rules.TryAddRule(firstRule);
            rules.TryAddRule(secondRule);
            rules.TryAddRule(thirdRule);
            rules.TryAddRule(fourthRule);
            rules.TryAddRule(fifthRule);

            Console.WriteLine(rules.Calculate("bigRule", 170f, 83f));
            Console.WriteLine(rules.Calculate("bigRule", 182f, 94f));
            Console.WriteLine(rules.Calculate("bigRule", 185f, 105f));
            Console.WriteLine(rules.Calculate("tallRule", 179f));
            Console.WriteLine(rules.Calculate("tallRule", 180f));
            Console.WriteLine(rules.Calculate("tallRule", 181f));
            Console.WriteLine(rules.Calculate("fastRule", 32f));
            Console.WriteLine(rules.Calculate("fastRule", 40f));
            Console.WriteLine(rules.Calculate("goodRugbyPlayerRule", 190f, 110f, 34f));
            Console.WriteLine(rules.Calculate("fastFatTallRule", 35f, 110f, 183));
        }
    }
}
