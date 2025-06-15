using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class WordCountProcessor : IProcessor<string, string>
    {
        public string Process(string input, Context context)
        {
            context.Log($"Liczyłem słowa w: '{input}'");
            int wordCount = input.Split(new char[] { ' ', '_' }, StringSplitOptions.RemoveEmptyEntries).Length;
            context.Log($"Znaleziono {wordCount} słów.");
            return $"WORD_COUNT_RESULT: {wordCount} (Original: '{input}')";
        }
    }


}
