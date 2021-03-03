using DotnetModelFuzzer.Manipulations.StringManips;
using Xunit;

namespace DotnetModelFuzzer.Manipulations.Tests
{
    public class BasicStringGenerationTests
    {
        [Fact]
        public void Generation()
        {
            var manip = new BasicStringGeneration(10);
            var result = manip.Manipulate();

            Assert.NotNull(result);
            Assert.Equal(31144, result.Length);
        }
    }
}
