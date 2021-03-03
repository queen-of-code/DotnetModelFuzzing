using System;

namespace DotnetModelFuzzer.Fuzzer.Models
{
    /// <summary>
    /// Provides fuzzing for a provider key-value pair, based on a separated percentage chance
    /// of fuzzing a key vs fuzzing a value.
    /// </summary>
    public class KeyValuePairModel<TValueModel, TValueStrategy, TValue> : Model<KeyValuePairStrategy<TValueStrategy>, Tuple<string, TValue>, string>
        where TValueModel : Model<TValueStrategy, TValue, TValue>
        where TValue : class, new()
        where TValueStrategy : Strategy, new()
    {
        public KeyValuePairModel() : base() { }

        public KeyValuePairModel(KeyValuePairStrategy<TValueStrategy> strat, int randomSeed)
            : base(strat, randomSeed) { }

        public TValueModel ValueModel { get; set; }

        public override Tuple<string, TValue> Fuzz(Tuple<string, TValue> input = null)
        {
            string newKey = input?.Item1;
            if (Random.RollPercentage(Strategy.Key.Probability))
            {
                newKey = DoFuzzingWork(LoadedManipulations, newKey);
            }

            TValue newValue = input?.Item2;
            if (ValueModel != null)
                newValue = ValueModel.Fuzz(newValue);

            return new Tuple<string, TValue>(newKey, newValue);
        }
    }
}
