using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class ReplaceSpacesWithUnderscoresProcessor : IProcessor<string, string>
    {
        public string Process(string input, Context context)
        {
            context.Log($"Zastępuję spacje podkreśleniami w: '{input}'");
            return input.Replace(' ', '_');
        }
    }


}
