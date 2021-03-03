namespace DotnetModelFuzzer.Fuzzer.Models
{
    public sealed class KeyValuePairStrategy<TValueStrategy> : Strategy
        where TValueStrategy : Strategy
    {
        public StringStrategy Key { get; set; }

        public TValueStrategy Value { get; set; }

        public override bool IsValid()
        {
            return Key == null || Value == null;
        }

    }
}
