using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class SqlTableNameAttribute : Attribute, IOptionAttribute
    {
        public string Table { get; set; }

        public byte OptionSet { get; private set; }

        public SqlTableNameAttribute(string table, byte optionSet = 0)
        {
            Table = table;
            OptionSet = optionSet;
        }
    }
}
