using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Attributes
{
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SqlPropertyAttribute : Attribute, IOptionAttribute
    {
        public byte OptionSet { get; private set; }

        public string ColumnName { get; set; }
        public string Alias { get; set; }


        public SqlPropertyAttribute(byte optionSet = 0)
        {
            OptionSet = optionSet;
        }
    }
}
