﻿using SqlQueryGenerator.Attributes;
using System;
using System.Collections.Generic;
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
        /// Use this for quickly accessing the column name associated with a property.
        /// </summary>
        /// <returns>The column name associated with the given property.</returns>
        public static string COL(this object source, string propertyName, byte optionSet = 0)
        {
            var attr = ReflectionHelper.GetOptionAttributeFromProperty<SqlPropertyAttribute>(source.GetType().GetProperty(propertyName), optionSet);
            if (attr != null)
            {
                return attr.Column;
            }

            return propertyName;
        }
    }
}