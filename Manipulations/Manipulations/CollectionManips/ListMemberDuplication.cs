using System;
using System.Collections.Generic;

namespace DotnetModelFuzzer.Manipulations.CollectionManips
{
    public class ListMemberDuplication<U> : Manipulation<List<U>>, IListManipulation<U>
    {
        public ListMemberDuplication()
        {
        }

        public ListMemberDuplication(int seed) : base(seed)
        {
        }

        public override List<U> Manipulate(List<U> input)
        {
            if (input != null && input.Count > 0)
            {
                var index = Random.Next(0, input.Count);
                input.Add(input[index]);
            }

            return input;
        }
    }
}
