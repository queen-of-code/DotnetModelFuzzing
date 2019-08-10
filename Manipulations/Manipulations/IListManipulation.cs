using System.Collections.Generic;

namespace Fuzzing.Manipulations
{
    public interface IListManipulation<T>
    {
        bool Manipulate(ref List<T> input);
    }
}
