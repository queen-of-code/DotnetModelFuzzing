namespace Fuzzing.Fuzzer
{
    public sealed class StringFuzzer : Fuzzer<Strategy, string, string>
    {
        public StringFuzzer() : base() { }

        public StringFuzzer(Strategy strat, int randomSeed)
            : base(strat, randomSeed) { }

        public override string Fuzz(string input = null)
        {
            return DoFuzzingWork<string>(this.LoadedManipulations, input);
        }
    }
}
