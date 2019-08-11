using Fuzzing.Manipulations.VulnerabilityManips;
using Xunit;

namespace ManipulationsTests
{
    public class ControlCharsInjectionTests
    {
        [Theory]
        [InlineData("abc12345", "ab%6ac12345")]
        [InlineData(null, "%6a")]
        [InlineData("a", "%6aa")]
        [InlineData("", "%6a")]
        public void Replace(string input, string expected)
        {
            var manip = new ControlCharInjection(5);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
