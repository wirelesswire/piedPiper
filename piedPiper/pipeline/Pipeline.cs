using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PipelineSystem;


    public partial class PipelineSystem
    {


        public static class Pipeline
        {
            public static IBuildablePipeline<InputType, OutputType> Create<InputType, OutputType>(IProcessor<InputType, OutputType> processor)
            {
                return new TerminalPipeline<InputType, OutputType>(processor);
            }
        }



    }

