using piedPiper.pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piedPiper.implementacje.stringi
{

    public class WordReverserProcessor : IProcessor<string, string>
    {
        public string Process(string input, Context context)
        {
            context.Log($"Odwracam kolejność słów w: '{input}'");
            string[] words = input.Split(new char[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(words);
            return string.Join(" ", words); 
        }
    }


}
