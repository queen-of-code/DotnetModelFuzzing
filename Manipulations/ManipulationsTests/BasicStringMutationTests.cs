using Fuzzing.Manipulations.StringManips;
using Xunit;

namespace ManipulationsTests
{
    public class BasicStringMutationTests
    {
        [Fact]
        public void Mutate_NotNull()
        {
            const string input = "123abc";
            var manip = new BasicStringMutation(10);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(31150, result.Length);
        }

        [Fact]
        public void Mutate_Null()
        {
            const string input = null;
            var manip = new BasicStringMutation(10);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(31144, result.Length);
        }

        [Fact]
        public void Mutate_Empty()
        {
            const string input = "";
            var manip = new BasicStringMutation(10);
            var result = manip.Manipulate(input);

            Assert.NotNull(result);
            Assert.Equal(31144, result.Length);
        }
    }
}
