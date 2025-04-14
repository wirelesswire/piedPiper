﻿using FuzzySharp.MembershipFunctions.Functions;
using FuzzySharp.Operators.SNorm;
using static PipelineSystem;

namespace piedPiper.implementacje.Hipki
{
    public class HipekHeightProcessor : TriangleMembershipFunction<float>, IProcessor<ocenionyHipek, ocenionyHipek>
    {
        private TriangleMembershipFunction<float> triangleMembershipFunction;


        public HipekHeightProcessor(float a, float b, float c) : base(a, b, c)
        {
            triangleMembershipFunction = new TriangleMembershipFunction<float>(a, b, c);
        }
        public NormOperationAlgebraicSum<float> NormOperationAlgebraicSum { get; set; } = new NormOperationAlgebraicSum<float>();

        public ocenionyHipek Process(ocenionyHipek input, Context context)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }



            input.ocena = NormOperationAlgebraicSum.Calculate(  input.ocena  ,  triangleMembershipFunction.GetMembership(input.hipek.height));


            return input;

        }
    }





}
