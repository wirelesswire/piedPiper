using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PipelineSystem;
using piedPiper.pipeline;

namespace piedPiper.implementacje.Hipki
{
    public class HipekOcenaOgolnaProcessor : IProcessor<ocenionyHipek, string>
    {
        private float passingScore = 0;
        public HipekOcenaOgolnaProcessor(float passingScore)
        {
            this.passingScore = passingScore;
        }
        public string Process(ocenionyHipek input, Context context)
        {
            if (input.ocena >= passingScore)
            {
                return "";
            }
            else
            {
                return "subhuman filth";
            }

        }
    }





}
