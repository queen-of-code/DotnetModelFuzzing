using System.Collections.Generic;

namespace Fuzzing.Manipulations
{
    public interface IListManipulation<T>
    {
        List<T> Manipulate(List<T> input);
    }
}
