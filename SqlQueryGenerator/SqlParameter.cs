using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator
{
    public class SqlParameter : ISqlParameter
    {
        public string QueryString { get; set; }
        public object QueryObject { get; set; }

        public SqlParameter(string queryString = null, object queryObject = null)
        {
            QueryString = queryString;
            QueryObject = queryObject;
        }
    }
}
