using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator
{
    public interface ISqlAccessProvider : IDisposable
    {
        List<J> Query<T, J>(T sqlParameter) where T : ISqlParameter;
        int Execute<T>(T sqlParameter) where T : ISqlParameter;
    }
}
