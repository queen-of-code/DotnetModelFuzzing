using Fuzzing.Manipulations.VulnerabilityManips;
using Xunit;

namespace ManipulationsTests
{
    public class SqlInjectionTests
    {
        [Theory]
        [InlineData("abc12345", "abx' OR full_name LIKE '%Bob%c12345")]
        [InlineData(null, "x' OR full_name LIKE '%Bob%")]
        [InlineData("a", "x' OR full_name LIKE '%Bob%a")]
        [InlineData("", "x' OR full_name LIKE '%Bob%")]
        public void Replace(string input, string expected)
        {
            var manip = new SqlInjection(12);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}
