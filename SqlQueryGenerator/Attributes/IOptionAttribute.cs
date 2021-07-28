using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Attributes
{
    public interface IOptionAttribute
    {
        byte OptionSet { get; }
    }
}
