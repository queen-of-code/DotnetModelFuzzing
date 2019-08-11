using Fuzzing.Manipulations;
using System;
using System.Linq;

namespace Fuzzing.Fuzzer
{
    public sealed class StringFuzzer : Fuzzer<string>
    {
        public StringFuzzer() : base() { }

        public StringFuzzer(Strategy strat, int randomSeed)
            : base(strat, randomSeed) { }

        public override string Fuzz(string input = null)
        {
            var manips = this.Random.Next(0, Strategy.MaxManipulations);

            var validManips = this.LoadedManipulations.Where(m => m is Manipulation<string>).ToArray();
            if (!validManips.Any())
                return null;

            string fuzzed = input;
            for (int x = 0; x < manips; x++)
            {
                int whichManip = Random.Next(0, validManips.Length);
                var manip = validManips[whichManip];
                fuzzed = manip.Manipulate(fuzzed);
            }

            return fuzzed;
        }
    }
}
