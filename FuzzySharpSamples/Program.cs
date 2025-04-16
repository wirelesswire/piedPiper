using FuzzySharp;
using FuzzySharp.MembershipFunctions;
using FuzzySharp.MembershipFunctions.Functions;

namespace FuzzySharpSamples
{
    public class Program
    {
        static void Main(string[] args)
        {
            float LeftFunc(float x)
            {
                return float.CreateTruncating(Math.Abs(Math.Cos(x)));
            }

            float RightFunc(float x)
            {
                return float.CreateTruncating(Math.Abs(Math.Sin(x)));
            }

            var mbFunction1 = new TrapezeMembershipFunction<float>(1,2,3,4);
            var mbFunction2 = new GaussMembershipFunction<float>(2.5f,2,3);
            var mbFunction3 = new TriangleMembershipFunction<float>(1,2.5f,4);
            var mbFunction4 = new BellMembershipFunction<float>(2,2,3);
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

            Console.WriteLine("Hello, World!");

            var dictionary = new Dictionary<float, float>();
            var dictionary2 = new Dictionary<float, float>();

            dictionary.Add(1,1);
            dictionary.Add(2,2);
            dictionary2.Add(3, 3);
            dictionary2.Add(4, 4);

            float Func(float arg1, float arg2)
            {
                return Math.Abs((arg1 - arg2) / (arg1*arg2));
            }

            var fuzzySet1 = new FuzzySet<float>(dictionary);
            var fuzzySet2 = new FuzzySet<float>(dictionary2);
            var fuzzyRelation = new FuzzyRelation<float>(fuzzySet1, fuzzySet2, Func);

            fuzzyRelation.CalculateRelation(1, 3);
            fuzzyRelation.CalculateRelation(1, 2);
        }

        public static int UseSampleClass() 
        {
            return ExampleClass.Add(5, 10);
        }
    }
}
