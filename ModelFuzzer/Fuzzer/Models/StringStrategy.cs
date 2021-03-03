using Newtonsoft.Json;

namespace DotnetModelFuzzer.Fuzzer.Models
{
    public sealed class StringStrategy : Strategy
    {
        /// <summary>
        /// The probability, from 0 to 100, that this field will get fuzzed. It will
        /// be rolled against like a dice roll.
        /// </summary>
        [JsonProperty("probability")]
        public ushort Probability { get; set; } = 10;
    }
}
