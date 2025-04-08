using FuzzySharp.MembershipFunctions.Functions;
using static PipelineSystem;

namespace piedPiper.implementacje.Hipki
{
    public class HipekHeightProcessor : TriangleMembershipFunction<float>, IProcessor<ocenionyHipek, ocenionyHipek>
    {
        private float waga = 0;
        private TriangleMembershipFunction<float> triangleMembershipFunction;

        //public HipekHeightProcessor(List<float> args) : base(args)
        //{
        //    //waga = 1;


        //}

        public HipekHeightProcessor(float a, float b, float c, float waga) : base(a, b, c)
        {
            waga = 1;
            triangleMembershipFunction = new TriangleMembershipFunction<float>(a, b, c);
        }

        //public HipekHeightProcessor(float waga)
        //{
        //    this.waga = waga;   
        //}

        public ocenionyHipek Process(ocenionyHipek input, Context context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            //if(input.hipek.height > 170)
            //{
            //input.ocena += 1*triangleMembershipFunction.CalculateMembership(input.hipek.height);
            input.ocena += 1 * triangleMembershipFunction.GetMembership(input.hipek.height);

            //}

            return input;

            //throw new NotImplementedException();
        }
    }





}
