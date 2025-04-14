using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PipelineSystem;

namespace piedPiper.pipeline
{

//szczerze nie mam pojęcia o czym ja tu myślałem 
    internal class ThrededEnumerableProcessor<InputType, OutputType,T> : IProcessor<InputType, OutputType> where InputType : IEnumerable<T>
    {
        public OutputType Process(InputType input, Context context)
        {

            //IEnumerable<T> outputList = new T[input.Count()];
            

            //for (int i = 0; i < input.Count(); i++)
            //{
            //    Task.Run(() =>
            //    {
                    
            //        // Simulate some processing
            //        context.Log($"Processing item: {i}");
            //        outputList[i] = input.ElementAt(i);
            //    });
            //}


            throw new NotImplementedException();
        }
    }

}
