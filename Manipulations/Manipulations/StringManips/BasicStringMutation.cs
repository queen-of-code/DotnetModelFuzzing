using System;
using Fuzzing.Manipulations;

namespace Manipulations.StringManips
{
    public class BasicStringMutation : Manipulation<string>, IMutationManipulation<string>
    {
        public BasicStringMutation() : base() { }
        public BasicStringMutation(int seed) : base(seed) { }

        public override string Manipulate(string input)
        {
            int length = Random.Next(1, short.MaxValue);
            var newString = GenerateRandomAsciiString(length);

            if (string.IsNullOrEmpty(input))
                return newString;

            int index = Random.Next(0, input.Length);

            return input.Insert(index, newString);
        }
    }
}
