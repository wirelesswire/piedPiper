using piedPiper.pipeline;

namespace piedPiper.implementacje.stringi
{
    public class AddPrefixProcessor : IProcessor<string, string>
    {
        private readonly string prefix;
        public AddPrefixProcessor(string prefix) { this.prefix = prefix; }
        public string Process(string input, Context context)
        {
            context.Log($"Dodaję prefiks '{prefix}' do: '{input}'");
            return prefix + input;
        }
    }


}
