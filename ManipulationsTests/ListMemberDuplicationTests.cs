using Fuzzing.Manipulations.CollectionManips;
using System.Collections.Generic;
using Xunit;

namespace ManipulationsTests
{
    public class ListMemberDuplicationTests
    {
        private static readonly List<string> empty = new List<string>();
        private static readonly List<string> one = new List<string>() { "abc" };
        private static readonly List<string> three = new List<string>() { "abc", "def", "ghi" };

        public static IEnumerable<object[]> GetData()
        {
            yield return new object[] { empty, 0 };
            yield return new object[] { null, null };
            yield return new object[] { one, 2 };
            yield return new object[] { three, 4 };
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Replace(List<string> input, int? expectedLength)
        {
            var manip = new ListMemberDuplication<string>(5);
            var result = manip.Manipulate(ref input);

            if (expectedLength.HasValue)
            {
                Assert.Equal(expectedLength.Value, input.Count);
            }
            else
            {
                Assert.Null(input);
            }
        }
    }
}
