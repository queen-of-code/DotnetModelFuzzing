using DotnetModelFuzzer.Fuzzer;
using DotnetModelFuzzer.Fuzzer.Models;
using DotnetModelFuzzer.Manipulations.StringManips;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DotnetModelFuzzer.Tests
{
    public class FuzzerTests
    {
        [Fact]
        public void LoadManipulations_Strings()
        {
            var strat = new StringStrategy
            {
                Name = "whatever",
                ValidManipulations = new List<string>
                {
                    "BasicStringMutation"
                }
            };

            var fuzzer = new StringModel(strat, 5);
            var result = fuzzer.LoadedManipulationsCount();
            Assert.Equal(1, result);

            var manip = fuzzer.GetManipsForTesting();
            Assert.Equal(typeof(BasicStringMutation), manip.First().GetType());
        }

        [Fact]
        public void LoadManipulations_Invalid()
        {
            var fuzzer = new TestFuzzer(
                new Strategy
                {
                    ValidManipulations = new List<string>
                    {
                        "BasicStringMutation"
                    }
                });

            var result = fuzzer.LoadedManipulationsCount();
            Assert.Equal(0, result);
        }

        private class TestFuzzer : Model<Strategy, object, object>
        {
            public TestFuzzer(Strategy strat) : base(strat, 5) { }

            public override object Fuzz(object input)
            {
                return new object();
            }
        }
    }

}
