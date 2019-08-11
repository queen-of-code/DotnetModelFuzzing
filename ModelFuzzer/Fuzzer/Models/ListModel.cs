using System.Collections.Generic;

namespace Fuzzing.Fuzzer.Models
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
                if (fuzzCount >= this.Strategy.MaxItemsToFuzz)
                    break;

                if (this.Random.RollPercentage(this.Strategy.Probability))
                {
                    modified.Add(this.DoFuzzingWork<T>(this.LoadedManipulations, item));
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
