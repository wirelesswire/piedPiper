using FuzzySharp;
using FuzzySharp.MembershipFunctions.Functions;

namespace FuzzySharpSamples
{
    public class Program
    {
        static void Main(string[] args)
        {
            var elo = new TriangleMembershipFunction<double>(3.3, 5, 7.2);
            var elo1 = new TriangleMembershipFunction<float>(3.3f, 5f, 7.2f);
            var elo2 = new TriangleMembershipFunction<decimal>(3.3M, 5M, 7.2M);

            var elo3 = new TrapezeMembersipFunction<decimal>(1,2,3,4);
            var elo4 = new TrapezeMembersipFunction<decimal>(new List<decimal>() {1, 2, 3, 4});
            var elo5 = new GaussMembershipFunction<decimal>(3.3M, 2, 7);
            var elo6 = new GaussMembershipFunction<decimal>(new List<decimal>() { 1, 2, 3, 4, 5 });


            Console.WriteLine("Hello, World!");
        }

        public static int UseSampleClass() 
        {
            return ExampleClass.Add(5, 10);
        }
    }
}
