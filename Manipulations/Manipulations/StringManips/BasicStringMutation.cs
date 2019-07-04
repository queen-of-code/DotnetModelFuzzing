﻿using System;

namespace Manipulations.StringManips
{
    public class BasicStringMutation : Manipulation, IMutationManipulation<string>
    {
        public BasicStringMutation() : base() { }
        public BasicStringMutation(int seed) : base(seed) { }

        public string Manipulate(string input)
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