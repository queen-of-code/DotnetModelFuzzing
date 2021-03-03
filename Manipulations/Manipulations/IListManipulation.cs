using System.Collections.Generic;

namespace DotnetModelFuzzer.Manipulations
{
    public interface IListManipulation<T>
    {
        List<T> Manipulate(List<T> input);
    }
}
