using SqlQueryGenerator.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SqlQueryGenerator.Helpers
{
    public static class QueryHelper
    {
        /// <summary>
        /// Use this for quickly accessing the table name associated with an object/it's type.
        /// </summary>
        /// <returns>The table name associated with the given object/it's type.</returns>
        public static string TBL(this object source, byte optionSet = 0)
        {
            return ReflectionHelper.GetTableName(source.GetType(), optionSet);
        }
        /// <summary>
        /// Use this for quickly accessing the table name associated with an object/it's type.
        /// </summary>
        /// <returns>The table name associated with the given object/it's type.</returns>
        public static string TBL(this Type source, byte optionSet = 0)
        {
            return ReflectionHelper.GetTableName(source, optionSet);
        }

        /// <summary>
        /// Use this for quickly accessing the column name associated with a property.
        /// </summary>
        /// <returns>The column name associated with the given property.</returns>
        public static string COL(this object source, string propertyName, byte optionSet = 0)
        {
            return ReflectionHelper.GetColumnName(source.GetType().GetMember(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)[0], optionSet);
        }
        /// <summary>
        /// Use this for quickly accessing the column name associated with a property.
        /// </summary>
        /// <returns>The column name associated with the given property.</returns>
        public static string COL(this Type source, string propertyName, byte optionSet = 0)
        {
            return ReflectionHelper.GetColumnName(source.GetMember(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)[0], optionSet);
        }


    }
}
