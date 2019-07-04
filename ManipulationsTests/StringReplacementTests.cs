using Fuzzing.Manipulations.StringManips;
using Xunit;

namespace ManipulationsTests
{
    public class StringReplacementTests
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("abc12345", "aaaa2345")]
        [InlineData("x", "a")]
        public void Replace(string input, string expected)
        {
            var manip = new StringReplacement(5);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(input?.Length ?? 0, result.Length);
            Assert.Equal(expected, result);
        }
    }
}
