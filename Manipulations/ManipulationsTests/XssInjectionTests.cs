using DotnetModelFuzzer.Manipulations.VulnerabilityManips;
using Xunit;

namespace DotnetModelFuzzer.Manipulations.Tests
{
    public class XssInjectionTests
    {
        [Theory]
        [InlineData("abc12345", "<IMG SRC= onmouseover=\"alert('xxs')\">abc12345")]
        [InlineData(null, "<IMG SRC= onmouseover=\"alert('xxs')\">")]
        [InlineData("a", "<IMG SRC= onmouseover=\"alert('xxs')\">a")]
        [InlineData("", "<IMG SRC= onmouseover=\"alert('xxs')\">")]
        public void Replace(string input, string expected)
        {
            var manip = new XssInjection(120);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
