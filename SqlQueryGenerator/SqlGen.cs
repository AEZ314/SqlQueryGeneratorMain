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


        public SqlParameter BuildQuery(string sqlStr, int limit = -1, int offset = -1, params ISqlProperty[] sqlProperties)
        {
            var limitStr = limit == -1 ? "" : $" LIMIT {limit} ";
            var offsetStr = offset == -1 ? "" : $" OFFSET {offset} ";
            var editedSqlStr = $"{sqlStr}{limitStr}{offsetStr}";
            return GenerateSqlParameter(editedSqlStr, sqlProperties);
        }
        public SqlParameter BuildQuery(string sqlStr, int limit = -1, int offset = -1, bool findNestedObjects = true)
        {
            var limitStr = limit == -1 ? "" : $" LIMIT {limit} ";
            var offsetStr = offset == -1 ? "" : $" OFFSET {offset} ";
            var editedSqlStr = $"{sqlStr}{limitStr}{offsetStr}";
            var sqlProperties = ReflectionHelper.GetAnyPropertyNames(QueryObject, findNestedObjects);
            return GenerateSqlParameter(editedSqlStr, sqlProperties.ToArray());
        }

        public SqlParameter AutoInsert(byte optionSet = 0, bool findNestedObjects = true)
        {
            string tableName = ReflectionHelper.GetTableName(QueryObject.GetType(), optionSet);
            var sqlProperties = ReflectionHelper.GetSqlProperties(QueryObject, optionSet, findNestedObjects);
            return ManualInsert(tableName, sqlProperties.ToArray());
        }
        public SqlParameter ManualInsert(string tableName, params ISqlProperty[] sqlProperties)
        {
            string columns = GenerateParenthStr(sqlProperties, (x) => x.Column);            // Making these three constructions separate may give independence but it comes with a performence cost, maybe change this.
            string properties = GenerateParenthStr(sqlProperties, (x) => $"@{x.Property}");
            string sqlStr = $"INSERT INTO {tableName} {columns} \n values {properties};";

            return GenerateSqlParameter(sqlStr, sqlProperties);
        }
        
        public SqlParameter GenerateSqlParameter(string sqlStr, params ISqlProperty[] sqlProperties)
        {
            object queryObject = GenerateSqlObject(sqlProperties);
            return new SqlParameter(sqlStr, queryObject);
        }
        public SqlParameter GenerateSqlParameter(string sqlStr, bool findNestedObjects = true)
        {
            var sqlProperties = ReflectionHelper.GetAnyPropertyNames(QueryObject, findNestedObjects);
            return GenerateSqlParameter(sqlStr, sqlProperties.ToArray());
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
        private static Dictionary<string, object> GenerateSqlObject(params ISqlProperty[] sqlProperties)
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