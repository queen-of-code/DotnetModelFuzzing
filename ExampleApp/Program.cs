using Fuzzing.Fuzzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExampleApp
{
    public class Program
    {
        private const int NumberRequests = 5;

        public static async Task<int> Main(string[] args)
        {
            var strategy = HttpRequestStrategy.GenerateDefaultStrategy();
            strategy.QueryParam.Probability = 80;
            var model = new HttpRequestModel(strategy, 10003); // Predictable random seed.

            // First make some nice and simple HTTP requests.
            var requests = new HttpRequestMessage[NumberRequests];
            for (int x = 0; x < NumberRequests; x++)
            {
                var uri = new UriBuilder("https://www.bing.com/search");
                uri.Query = $"q=ponies&count={x}";
                
                var message = new HttpRequestMessage(HttpMethod.Get, uri.Uri);
                message.Headers.Add("X-Custom-Header", "FuzzTest");
                message.Headers.Add("Accept", "text/html, application/xhtml+xml, application/xml");
                message.Headers.Add("accept-encoding", "gzip, deflate, br");
                message.Headers.Add("accept-language", "en-US,en;q=0.9");
                message.Headers.Add("cache-control", "max-age=0");
                message.Headers.Add("Cookie", "SRCHD=AF=NOFORM;MUID=" + Guid.NewGuid().ToString("N"));

                requests[x] = message;

                Console.WriteLine("Generated: " + message.ToString());
            }

            Console.WriteLine("Press enter to fuzz them!");
            Console.ReadLine();

            // Now fuzz those requests!
            var fuzzed = new List<HttpRequestMessage>();
            foreach (var req in requests)
            {
                var fuzz = model.Fuzz(req);
                fuzzed.Add(fuzz);
                Console.WriteLine("Fuzzed: " + fuzz.ToString());
            }

            Console.WriteLine("Press enter to send these requests!");
            Console.ReadLine();

            using (var client = new HttpClient())
            {
                var tasks = new Task<HttpResponseMessage>[NumberRequests];
                for (int x = 0; x < fuzzed.Count; x++)
                {
                    tasks[x] = client.SendAsync(fuzzed[x]);
                }

                foreach (var t in tasks.ToList())
                {
                    try
                    {
                        var result = await t;
                        Console.WriteLine("Response Status Code " + result.StatusCode);
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine("Response Exception: " + e.Message);
                    }
                }

                Console.ReadLine();
            }

            return 0;
        }

    }
}
