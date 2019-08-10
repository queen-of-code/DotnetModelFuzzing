using Fuzzing.Manipulations.VulnerabilityManips;
using Xunit;

namespace ManipulationsTests
{
    public class JsonInjectionTests
    {
        [Theory]
        [InlineData("abc12345", "ab{\"constructor\":\"null\"}c12345")]
        [InlineData(null, "{\"constructor\":\"null\"}")]
        [InlineData("a", "{\"constructor\":\"null\"}a")]
        [InlineData("", "{\"constructor\":\"null\"}")]
        public void Replace(string input, string expected)
        {
            var manip = new JsonInjection(5);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
