using Fuzzing.Manipulations.VulnerabilityManips;
using Xunit;

namespace ManipulationsTests
{
    public class FormatStringsInjectionTests
    {
        [Theory]
        [InlineData("abc12345", "abc12%20x345")]
        [InlineData(null, "%20x")]
        [InlineData("a", "%20xa")]
        [InlineData("", "%20x")]
        public void Replace(string input, string expected)
        {
            var manip = new FormatStringsInjection(20);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
