using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Attributes
{
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SqlPropertyAttribute : Attribute, IOptionAttribute
    {
        public string ColumnName { get; set; }

        public byte OptionSet { get; private set; }

        public SqlPropertyAttribute(byte optionSet = 0)
        {
            OptionSet = optionSet;
        }
    }
}
