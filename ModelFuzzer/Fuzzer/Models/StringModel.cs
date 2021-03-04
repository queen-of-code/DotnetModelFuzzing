namespace DotnetModelFuzzer.Fuzzer.Models
{
    public sealed class StringModel : Model<StringStrategy, string, string>
    {
        public StringModel() : base() { }

        public StringModel(StringStrategy strat, int randomSeed)
            : base(strat, randomSeed) { }

        public override string Fuzz(string input = null)
        {
            if (Random.RollPercentage(Strategy.Probability))
            {
                return DoFuzzingWork(LoadedManipulations, input);
            }

            return input;
        }
    }
}
