using DotnetModelFuzzer.Manipulations.StringManips;
using Xunit;

namespace DotnetModelFuzzer.Manipulations.Tests
{
    public class StringReplacementTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("abc12345", "abaaac12345")]
        [InlineData("x", "ax")]
        public void Replace(string input, string expected)
        {
            var manip = new StringReplacement(5);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
