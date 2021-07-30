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


        /// <summary>
        /// Use this when you already have a query object and don't need a generated query object such as <see cref="SqlParameter.QueryObject"/>. 
        /// Usage example: sqlGen.ManualInsert<SqlProperty>("table", ("A", "a"), ("B", "b"), ("C", "c"));
        /// </summary>
        public static implicit operator SqlProperty((string Column, string Property) tuple)
        {
            return new SqlProperty()
            {
                Column = tuple.Column,
                Property = tuple.Property,
                Value = null,
            };
        }
        /// <summary>
        /// This conversion operator seeks to provide ease of use. Usage example: sqlGen.ManualInsert<SqlProperty>("table", ("A", "a", "1"), ("B", "b", "2"), ("C", "c", "3"));
        /// </summary>
        public static implicit operator SqlProperty((string Column, string Property, object Value) tuple)
        {
            return new SqlProperty()
            {
                Column = tuple.Column,
                Property = tuple.Property,
                Value = tuple.Value,
            };
        }
    }
}
