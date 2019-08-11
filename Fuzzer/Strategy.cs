using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fuzzing.Fuzzer
{
    public class Strategy
    {
        /// <summary>
        /// A unique name for this strategy.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = "Default";

        /// <summary>
        /// A list of the simple names of the manipulations that are to be considered 
        /// in-play for this fuzzing strategy. For example, `BasicStringMutation`.
        /// </summary>
        [JsonProperty("valid_manipulations")]
        public List<string> ValidManipulations { get; set; } = new List<string>();

        /// <summary>
        /// The maximum number of manipulations to apply to any one field that is being fuzzed.
        /// </summary>
        [JsonProperty("max_manipulations")]
        public int MaxManipulations { get; set; } = 5;


        public Strategy()
        {
        }
    }  
}
