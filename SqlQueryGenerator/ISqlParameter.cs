using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator
{
    public interface ISqlParameter
    {
        string QueryString { get; set; }
        object QueryObject { get; set; }
    }
}
