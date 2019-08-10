﻿using System;

namespace Fuzzing.Manipulations.StringManips
{
    public class StringReplacement : Manipulation, IMutationManipulation<string>
    {
        public StringReplacement() : base() { }
        public StringReplacement(int seed) : base(seed) { }

        public string Manipulate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            int length = Random.Next(1, input.Length + 1);
            var newString = GenerateRandomAsciiString(length);

            return this.InsertString(input, newString);
        }
    }
}
