using System;
using System.Net.Http;

namespace Fuzzing.Fuzzer
{
    /// <summary>
    /// Provides intelligent fuzzing of an HttpRequestMessage, which can then be sent using an
    /// HttpClient.
    /// </summary>
    public sealed class HttpRequestFuzzer : Fuzzer<HttpRequestStrategy, HttpRequestMessage, string>
    {
        public override HttpRequestMessage Fuzz(HttpRequestMessage input = null)
        {
            if (input == null)
                return null;

            // Fuzz JUST the path of the URI
            if (this.Random.RollPercentage(this.Strategy.PathFuzzChance))
            {
                var path = DoFuzzingWork<string>(this.LoadedManipulations, input.RequestUri.AbsolutePath);
                var builder = new UriBuilder(input.RequestUri);
                builder.Path = path;
                input.RequestUri = builder.Uri;
            }

            // Fuzz JUST the queryparam section of the URI. It might be better to one day split the 
            // query line into KVP and fuzz each individually, but this is good enough for now.
            if (this.Random.RollPercentage(this.Strategy.QueryParamFuzzChance))
            {
                var query = DoFuzzingWork<string>(this.LoadedManipulations, input.RequestUri.Query);
                var builder = new UriBuilder(input.RequestUri);
                builder.Query = query;
                input.RequestUri = builder.Uri;
            }

            return input;
        }
    }
}
