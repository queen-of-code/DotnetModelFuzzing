using System.Collections.Generic;

namespace DotnetModelFuzzer.Fuzzer.Models
{
    public sealed class ListModel<T, TStrategy> : Model<ListStrategy<TStrategy>, IList<T>, T>
        where T : new()
        where TStrategy : Strategy
    {
        public override IList<T> Fuzz(IList<T> input = null)
        {
            var modified = new List<T>();
            var fuzzCount = 0;
            foreach (var item in input)
            {
                // Only fuzz  as many items as we set in the overall cap
                if (fuzzCount >= Strategy.MaxItemsToFuzz)
                    break;

                if (Random.RollPercentage(Strategy.Probability))
                {
                    modified.Add(DoFuzzingWork(LoadedManipulations, item));
                    ++fuzzCount;
                }
                else
                {
                    modified.Add(item);
                }

            }

            return modified;
        }
    }
}
