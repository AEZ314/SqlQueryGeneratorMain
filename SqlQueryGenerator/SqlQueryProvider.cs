using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator
{
    public class SqlQueryProvider : ISqlQueryProvider
    {
        public ISqlAccessProvider SqlAccessProvider { get; set; }


        public SqlQueryProvider(ISqlAccessProvider sqlAccessProvider)
        {
            SqlAccessProvider = sqlAccessProvider;
        }


        public List<T> Query<T>(ISqlParameter sqlParameter)
        {
            return SqlAccessProvider.Query<T>(sqlParameter.QueryString, sqlParameter.QueryObject);
        }

        public int Execute(ISqlParameter sqlParameter)
        {
            return SqlAccessProvider.Execute(sqlParameter.QueryString, sqlParameter.QueryObject);
        }
    }
}
