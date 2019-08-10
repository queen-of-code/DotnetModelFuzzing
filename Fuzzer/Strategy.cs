using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fuzzing.Fuzzer
{
    public class Strategy
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "Default";

        [JsonProperty("valid_manipulations")]
        public List<string> ValidManipulations { get; set; } = new List<string>();

        [JsonProperty("max_manipulations")]
        public int MaxManipulations { get; set; } = 5;
    }
}
