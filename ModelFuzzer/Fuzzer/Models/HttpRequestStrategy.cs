using System.Collections.Generic;

namespace Fuzzing.Fuzzer.Models
{
    public sealed class HttpRequestStrategy : Strategy
    {
        //public KeyValuePairStrategy<Strategy> QueryParam { get; set; }
        public StringStrategy QueryParam { get; set; }

        public StringStrategy Path { get; set; }

        public KeyValuePairStrategy<ListStrategy<StringStrategy>> Headers { get; set; }

        public List<string> HeaderWhitelist { get; set; } = new List<string>();

        /// <summary>
        /// Generates a sane default strategy for fuzzing HTTP requests in dotnet.
        /// </summary>
        public static HttpRequestStrategy GenerateDefaultStrategy()
        {
            var strat = new HttpRequestStrategy
            {
                //QueryParam = new KeyValuePairStrategy<Strategy>
                //{
                //    MaxManipulations = 2,
                //    Key = new StringStrategy
                //    {
                //        Probability = 20
                //    },
                //    Value = new StringStrategy
                //    {
                //        Probability = 20
                //    }
                //},
                QueryParam = new StringStrategy
                {
                    MaxManipulations = 2,
                    Probability = 25,
                    UseAllRelevantManipulations = false,
                    ValidManipulations = new List<string>
                    {
                        "FuzzDbManipulation",
                        "ControlCharInjection",
                        "XssInjection",
                        "FormatStringsInjection"
                    }
                },
                Path = new StringStrategy
                {
                    MaxManipulations = 2,
                    Probability = 5,
                    UseAllRelevantManipulations = false,
                    ValidManipulations = new List<string>
                    {
                        "FuzzDbManipulation",
                        "ControlCharInjection",
                        "XssInjection",
                        "FormatStringsInjection"
                    }
                },
                Headers = new KeyValuePairStrategy<ListStrategy<StringStrategy>>
                {
                    Key = new StringStrategy
                    {
                        UseAllRelevantManipulations = true,
                        MaxManipulations = 1,
                        Probability = 5
                    },
                    Value = new ListStrategy<StringStrategy>
                    {
                        UseAllRelevantManipulations = true,
                        MaxManipulations = 1,
                        MaxItemsToFuzz = 2,
                        Probability = 10
                    },
                }
            };

            strat.UseAllRelevantManipulations = true;
            strat.HeaderWhitelist = new List<string> { "Cookie", "Authorization" };

            return strat;
        }

        public override bool IsValid()
        {
            if (this.Path == null || this.QueryParam == null)
                return false;

            if (this.Headers == null || this.Headers.Key == null || this.Headers.Value == null)
                return false;

            return true;
        }
    }
}
