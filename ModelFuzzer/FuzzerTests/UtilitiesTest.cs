using Fuzzing.Fuzzer;
using System;
using System.Threading;
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



        public bool IsEvenNumber(int number)
        {
            return (number & 1) == 0;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(Int32.MaxValue)]
        public void EvenNumberTests(int numberToTest)
        {
            bool expectedEven = numberToTest % 2 == 0; // This is the Oracle

            bool actualValue = IsEvenNumber(numberToTest);

            Assert.Equal(expectedEven, actualValue);
        }
    }


}
