using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Attributes
{
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SqlPropertyAttribute : Attribute, IOptionAttribute
    {
        public byte OptionSet { get; private set; }
        public string Column { get; set; }

        public SqlPropertyAttribute(string column = null, byte optionSet = 0)
        {
            Column = column;
            OptionSet = optionSet;
        }
    }
}
