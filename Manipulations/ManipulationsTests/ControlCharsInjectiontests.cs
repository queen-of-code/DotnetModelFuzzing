using DotnetModelFuzzer.Manipulations.VulnerabilityManips;
using Xunit;

namespace DotnetModelFuzzer.Manipulations.Tests
{
    public class ControlCharsInjectionTests
    {
        [Theory]
        [InlineData("abc12345", "ab%69c12345")]
        [InlineData(null, "%69")]
        [InlineData("a", "%69a")]
        [InlineData("", "%69")]
        public void Replace(string input, string expected)
        {
            var manip = new ControlCharInjection(5);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
