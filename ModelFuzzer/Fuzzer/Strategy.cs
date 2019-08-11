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
        /// If this is set to true, the ValidManipulations setting will be ignored and
        /// ALL manipulations that are marked as valid for the given data type will be
        /// loaded as possibilities to use when crafting attacks.
        /// </summary>
        [JsonProperty("use_all_relevant_manipulations")]
        public bool UseAllRelevantManipulations { get; set; } = false;

        /// <summary>
        /// The maximum number of manipulations to apply to any one field that is being fuzzed.
        /// </summary>
        [JsonProperty("max_manipulations")]
        public ushort MaxManipulations { get; set; } = 5;


        public Strategy()
        {
        }

        /// <summary>
        /// Override this to do validity checks.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            return true;
        }
    }  
}
