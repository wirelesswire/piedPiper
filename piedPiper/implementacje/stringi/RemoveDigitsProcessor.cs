using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class RemoveDigitsProcessor : IProcessor<string, string>
    {
        public string Process(string input, Context context)
        {
            context.Log($"Usuwam cyfry z: '{input}'");
            return new string(input.Where(c => !char.IsDigit(c)).ToArray());
        }
    }


}
