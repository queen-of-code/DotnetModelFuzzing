using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Fuzzing.Fuzzer.Models
{
    /// <summary>
    /// Provides intelligent fuzzing of an HttpRequestMessage, which can then be sent using an
    /// HttpClient. Note that as HttpRequestMessage does NOT have a body on it, that this class
    /// will not fuzz a body. Please use an appropriate other fuzzer to handle that if required.
    /// </summary>
    public sealed class HttpRequestModel : Model<HttpRequestStrategy, HttpRequestMessage, string>
    {
        public HttpRequestModel() : base() { }
        public HttpRequestModel(HttpRequestStrategy strategy, int randomSeed) 
            : base(strategy, randomSeed) { }

        public override HttpRequestMessage Fuzz(HttpRequestMessage input = null)
        {
            if (input == null)
                return null;

            if (this.LoadedManipulations == null || this.LoadedManipulations.Count == 0)
                throw new ArgumentNullException("No manipulations loaded. Cannot fuzz!");
            

            if (this.Strategy == null || !this.Strategy.IsValid())
                throw new ArgumentException("No valid strategy was loaded. Cannot fuzz!");

            // Fuzz JUST the path of the URI
            if (this.Random.RollPercentage(this.Strategy.Path.Probability))
            {
                var path = DoFuzzingWork<string>(this.LoadedManipulations, input.RequestUri.AbsolutePath);
                var builder = new UriBuilder(input.RequestUri);
                builder.Path = path;
                input.RequestUri = builder.Uri;
            }

            // Fuzz JUST the queryparam section of the URI. It might be better to one day split the 
            // query line into KVP and fuzz each individually, but this is good enough for now.
            if (this.Random.RollPercentage(this.Strategy.QueryParam.Probability))
            {
                var query = DoFuzzingWork<string>(this.LoadedManipulations, input.RequestUri.Query);
                var builder = new UriBuilder(input.RequestUri);
                builder.Query = query;
                input.RequestUri = builder.Uri;
            }

            // Fuzz the headers in a somewhat intelligent way. 
            var headersToAdd = new List<Tuple<string, IEnumerable<string>>>();
            foreach (var header in input.Headers)
            {
                if (this.Strategy.HeaderWhitelist.Contains(header.Key))
                    continue;

                if (this.Random.RollPercentage(this.Strategy.Headers.Key.Probability))
                {
                    var newKey = DoFuzzingWork<string>(this.LoadedManipulations, header.Key);
                    headersToAdd.Add(new Tuple<string, IEnumerable<string>>(newKey, header.Value));
                }

                if (this.Random.RollPercentage(this.Strategy.Headers.Value.Probability))
                {
                    var values = new String[header.Value.Count()];
                    int index = 0;
                    foreach (var val in header.Value)
                    {
                        if (this.Random.RollPercentage(this.Strategy.Headers.Value.Probability))
                        {
                            values[index] = DoFuzzingWork<string>(this.LoadedManipulations, val);
                        }
                        else
                        {
                            values[index] = val;
                        }

                        ++index;
                    }

                    headersToAdd.Add(new Tuple<string, IEnumerable<string>>(header.Key, values));
                }
            }

            if (headersToAdd.Count > 0)
            {
                foreach(var pair in headersToAdd)
                {
                    try
                    {
                        input.Headers.Remove(pair.Item1);
                    }
                    catch (FormatException) { }
                    input.Headers.TryAddWithoutValidation(pair.Item1, pair.Item2);
                }
            }

            return input;
        }
    }
}
