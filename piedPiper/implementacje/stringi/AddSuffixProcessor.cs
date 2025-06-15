using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class AddSuffixProcessor : IProcessor<string, string>
    {
        private readonly string suffix;
        public AddSuffixProcessor(string suffix) { this.suffix = suffix; }
        public string Process(string input, Context context)
        {
            context.Log($"Dodaję sufiks '{suffix}' do: '{input}'");
            return input + suffix;
        }
    }


}
