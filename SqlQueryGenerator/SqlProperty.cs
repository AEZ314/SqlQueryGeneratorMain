using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator
{
    /// <summary>
    /// "Associates" DB and a property.
    /// </summary>
    public class SqlProperty : ISqlProperty
    {
        public string Column { get; set; }
        public string Property { get; set; }
        public object Value { get; set; }

        public SqlProperty(string column, string property, object value)
        {
            Column = column;
            Property = property;
            Value = value;
        }

        /// <summary>
        /// Use this when you already have a query object and don't need a generated query object such as <see cref="SqlParameter.QueryObject"/>. 
        /// Usage example: sqlGen.ManualInsert<SqlProperty>("table", ("A", "a"), ("B", "b"), ("C", "c"));
        /// </summary>
        public SqlProperty(string column, string property)
        {
            Column = column;
            Property = property;
            Value = null;
        }
    }
}
