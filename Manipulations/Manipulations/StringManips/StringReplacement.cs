using System;

namespace DotnetModelFuzzer.Manipulations.StringManips
{
    public class StringReplacement : Manipulation<string>, IMutationManipulation<string>
    {
        public StringReplacement() : base() { }
        public StringReplacement(int seed) : base(seed) { }

        public override string Manipulate(string input = default)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            int length = Random.Next(1, input.Length + 1);
            var newString = GenerateRandomAsciiString(length);

            return InsertString(input, newString);
        }
    }
}
