﻿using System;
using System.Collections.Generic;
using System.Text;
using SqlQueryGenerator.Attributes;

namespace SqlQueryGenerator
{
    public interface ISqlGen
    {
        /// <summary>
        /// The query object containing the properties to be referenced in the query string. Certain methods will require this property to be assigned a value.
        /// </summary>
        object QueryObject { get; set; }

        /// <summary>
        /// Manually generates an <see cref="SqlParameter"/> with query string for row insertion using an object's properties.
        /// </summary>
        /// <param name="tableName">Name of the table to act upon.</param>
        /// <param name="sqlProperties">These can be substituted with tuples of three of form (Column Name, Property Name, Property Value) 
        /// or alternatively (Column Name, Property Name) where the value is assumed to be null. Go with the latter if you don't want to use the returned <see cref="SqlParameter.QueryObject"/>.</param>
        /// <returns>An <see cref="SqlParameter"/> to be used for querying.</returns>
        SqlParameter ManualInsert<T>(string tableName, params T[] sqlProperties) where T : ISqlProperty;

        /// <summary>
        /// Generates an <see cref="SqlParameter"/> using the properties of <see cref="QueryObject"/> which are tagged with <see cref="SqlPropertyAttribute"/>.
        /// </summary>
        /// <param name="tableName">Name of the table to act upon.</param>
        /// <param name="optionSet">Used for choosing out of multiple <see cref="SqlPropertyAttribute"/> by <see cref="SqlPropertyAttribute.OptionSet"/>.</param>
        /// <param name="useNestedObjects">If true, the returned <see cref="SqlParameter"/> will also account for the sub-objects of <see cref="QueryObject"/> as well as it's own properties.</param>
        /// <returns>An <see cref="SqlParameter"/> for querying a DB. Be aware that the returned <see cref="SqlParameter.QueryObject"/> might differ from <see cref="QueryObject"/>.</returns>
        SqlParameter AutoInsert(string tableName, byte optionSet = 0, bool useNestedObjects = true);
    }
}