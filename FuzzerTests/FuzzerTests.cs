using Fuzzing.Fuzzer;
using Manipulations.StringManips;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FuzzerTests
{
    public class UnitTest1
    {
        [Fact]
        public void LoadManipulations()
        {
            var strat = new Strategy
            {
                Name = "whatever",
                ValidManipulations = new List<string>
                {
                    "BasicStringMutation"
                }
            };

            var fuzzer = new StringFuzzer(strat, 5);
            
            var result =  fuzzer.LoadManipulations();
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(typeof(BasicStringMutation), result.First().GetType());
        }
    }
}