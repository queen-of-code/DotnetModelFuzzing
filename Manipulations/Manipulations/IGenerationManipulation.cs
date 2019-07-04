using System;
using System.Collections.Generic;
using System.Text;

namespace Manipulations
{
    public interface IGenerationManipulation<T> 
    {
        T Manipulate();
    }
}
