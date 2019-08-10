using System;
using System.Collections.Generic;
using Fuzzing.Fuzzer;
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
            
            var result =  Fuzzer.LoadManipulations(strat);
            Assert.NotNull(result);
            Assert.Single(result);
        }
    }
}
