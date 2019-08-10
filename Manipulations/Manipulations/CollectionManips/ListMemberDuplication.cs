using System;
using System.Collections.Generic;

namespace Fuzzing.Manipulations.CollectionManips
{
    public class ListMemberDuplication<T> : Manipulation, IListManipulation<T> 
    {
        public ListMemberDuplication()
        {
        }

        public ListMemberDuplication(int seed) : base(seed)
        {
        }

        public bool Manipulate(ref List<T> input)
        {
            if (input != null && input.Count > 0)
            {
                var index = Random.Next(0, input.Count);
                input.Add(input[index]);
                return true;
            }

            return false;
        }
    }
}
