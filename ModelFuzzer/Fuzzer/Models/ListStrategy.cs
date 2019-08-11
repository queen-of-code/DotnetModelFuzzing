using Newtonsoft.Json;

namespace Fuzzing.Fuzzer.Models
{
    public sealed class ListStrategy<TValueStrategy> : Strategy where TValueStrategy : Strategy
    {
        /// <summary>
        /// The probability, from 0 to 100, that this field will get fuzzed. It will
        /// be rolled against like a dice roll. Note that EACH ITEM in the list will roll
        /// individually against this probability.
        /// </summary>
        [JsonProperty("probability")]
        public ushort Probability { get; set; } = 10;

        [JsonProperty("max_items_to_fuzz")]
        public ushort MaxItemsToFuzz { get; set; } = 5;
    }
}
