using System;
using System.Collections.Generic;
using System.Text;

namespace Fuzzing.Fuzzer
{
    public sealed class HttpRequestStrategy : Strategy
    {
        public int QueryParamFuzzChance { get; set; } = 20;

        public int HeaderKeyFuzzChance { get; set; } = 20;

        public int HeaderValueFuzzChance { get; set; } = 10;

        public int PathFuzzChance { get; set; } = 5;

        public int CookieFuzzChance { get; set; } = 20;

        public List<string> HeaderWhitelist { get; set; } = new List<string>();

        public List<string> HeaderBlacklist { get; set; } = new List<string>();
    }
}
