using piedPiper.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace piedPiper.implementacje.stringi
{
    public class AppendStringProcessor : IProcessor<string, string>
    {
        string stringToAppend;
        public AppendStringProcessor(string stringToAppend) {
            this.stringToAppend = stringToAppend;
        }
        public string Process(string input, Context context)
        {
            return input + stringToAppend;
        }
    }
}
