using System;
using System.Collections.Generic;
using System.Text;

namespace Manipulations
{
    public interface IMutationManipulation<T>
    {
        T Manipulate(T input);
    }
}
