using DotnetModelFuzzer.Fuzzer.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DotnetModelFuzzer.Tests
{
    public class HttpRequestFuzzingTest
    {

        [Fact]
        public async Task HttpClientTest()
        {
            var strat = HttpRequestStrategy.GenerateDefaultStrategy();
            strat.Path.Probability = 100;
            strat.Path.ValidManipulations = new List<string>
            {
                "BasicStringGeneration"
            };
            strat.ValidManipulations = new List<string>
            {
                "BasicStringGeneration"
            };

            var fuzzer = new HttpRequestModel(strat, 10); ;

            var message = new HttpRequestMessage(HttpMethod.Get, "https://www.bing.com");
            var fuzzMessage = fuzzer.Fuzz(message);

            Assert.NotEqual("/", fuzzMessage.RequestUri.AbsolutePath);

            using (var client = new HttpClient())
            {
                var response = await client.SendAsync(message);
                Assert.False(response.IsSuccessStatusCode);
                Assert.Equal("BadRequest", response.StatusCode.ToString());
            }
        }

    }
}
