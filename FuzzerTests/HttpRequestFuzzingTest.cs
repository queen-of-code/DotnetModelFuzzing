using System.Net.Http;
using Xunit;

namespace Fuzzer.FuzzerTests
{
    public class HttpRequestFuzzingTest
    {

        [Fact]
        public void HttpClientTest()
        {

            using (var client = new HttpClient())
            {
            }
        }

    }
}
