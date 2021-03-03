using System;

namespace DotnetModelFuzzer.Fuzzer
{
    /// <summary>
    /// Bag of holding for extension methods and other useful things.
    /// </summary>
    public static class Utilities
    {
        public static bool RollPercentage(this Random rand, int chance)
        {
            var roll = rand.Next(0, 101);
            return roll < chance;
        }
    }
}
