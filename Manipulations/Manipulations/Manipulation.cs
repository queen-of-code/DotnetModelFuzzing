using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fuzzer.ManipulationsTests.dll")]

namespace Fuzzing.Manipulations
{
    public abstract class Manipulation
    {
        protected Random Random;

        public Manipulation()
        {
            Random = new Random();
        }

        public Manipulation(int seed)
        {
            Random = new Random(seed);
        }

        /// <summary>
        /// Generates a simple ASCII string of the specified length. 
        /// </summary>
        /// <param name="length">How long of a string to return. Greater than 0. </param>
        /// <returns>It's just a string of 'a'.</returns>
        protected string GenerateRandomAsciiString(int length)
        {
            if (length <= 0)
                return string.Empty;

            return new string('a', length);
        }

        protected string GenerateRandomUtf8String(int length)
        {
            if (length <= 0)
                return string.Empty;

            return new string('\ud790', length);
        }
    }
}
