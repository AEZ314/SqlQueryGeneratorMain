using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator
{
    public interface ISqlQueryProvider
    {
        List<T> Query<T>(ISqlParameter sqlParameter);
        int Execute(ISqlParameter sqlParameter);
    }
}
