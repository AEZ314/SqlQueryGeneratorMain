using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator
{
    public interface ISqlAccessProvider
    {
        List<T> Query<T>(string sql, object parameter);
        int Execute(string sql, object parameter);
    }
}
