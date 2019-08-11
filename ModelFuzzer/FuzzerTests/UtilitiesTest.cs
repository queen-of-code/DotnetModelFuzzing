using Fuzzing.Fuzzer;
using System;
using Xunit;

namespace Fuzzer.FuzzerTests
{
    public class UtilitiesTest
    {
        [Theory]
        [InlineData(0, false)]
        [InlineData(100, true)]
        [InlineData(1, false)]
        [InlineData(99, true)]
        public void TestRollPercentage(int chance, bool passExpected)
        {
            var random = new Random(1);
            var result = random.RollPercentage(chance);
            Assert.Equal(passExpected, result);
        }

    }
}
