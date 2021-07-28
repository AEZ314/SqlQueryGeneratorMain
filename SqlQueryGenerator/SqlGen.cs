using System;
using System.Collections.Generic;
using SqlQueryGenerator.Helpers;

namespace SqlQueryGenerator
{
    public class SqlGen : ISqlGen
    {
        public object QueryObject { get; set; }


        public SqlGen(object queryObject = null)
        {
            QueryObject = queryObject;
        }


        public SqlParameter ManualInsert<T>(string tableName, params T[] sqlProperties) where T : ISqlProperty
        {
            string columns = GenerateParenthStr(sqlProperties, (x) => x.Column);            // Making these three constructions separate may give independence but it comes with a performence cost, maybe change this.
            string properties = GenerateParenthStr(sqlProperties, (x) => $"@{x.Property}");
            object queryObject = GenerateQueryObject(sqlProperties);

            return new SqlParameter($"INSERT INTO {tableName} {columns} \n values {properties};", queryObject);
        }

        public SqlParameter AutoInsert(string tableName, byte optionSet = 0, bool findNestedObjects = true)
        {
            var sqlProperties = ReflectionHelper.GetSqlProperties(QueryObject, optionSet, findNestedObjects);
            return ManualInsert(tableName, sqlProperties.ToArray());
        }



        private static string GenerateParenthStr<T>(IEnumerable<T> elements, Func<T, string> elementToStr = null, char seperator = ',')
        {
            elementToStr = elementToStr ?? ((x) => x.ToString());

            var innerStr = "";

            foreach(var element in elements)
            {
                innerStr += $"{elementToStr(element)}{seperator} ";
            }
            innerStr = innerStr.Trim(',', ' ');

            return $"({innerStr})";
        }
        private static Dictionary<string, object> GenerateQueryObject<T>(params T[] sqlProperties) where T : ISqlProperty
        {
            var dict = new Dictionary<string, object>();

            foreach(var sqlProperty in sqlProperties)
            {
                dict.Add(sqlProperty.Property, sqlProperty.Value);
            }

            return dict;
        }
    }
}
