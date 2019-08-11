using Fuzzing.Manipulations.VulnerabilityManips;
using Xunit;

namespace ManipulationsTests
{
    public class NoSqlInjectionTests
    {
        [Theory]
        [InlineData("abc12345", "ab[$ne]=1c12345")]
        [InlineData(null, "[$ne]=1")]
        [InlineData("a", "[$ne]=1a")]
        [InlineData("", "[$ne]=1")]
        public void Replace(string input, string expected)
        {
            var manip = new NoSqlInjection(12);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
