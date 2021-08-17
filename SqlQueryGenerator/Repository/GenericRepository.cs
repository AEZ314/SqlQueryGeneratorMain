using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private SqlGen _SqlGen;

        public SqlGen SqlGen => _SqlGen ?? (_SqlGen = new SqlGen());
        public ISqlAccessProvider SqlAccessProvider { get; set; }

        public GenericRepository(ISqlAccessProvider sqlAccessProvider)
        {
            SqlAccessProvider = sqlAccessProvider;
        }


        public void AutoInsert(object source, byte optionSet = 0, bool findNestedObjects = true)
        {
            SqlGen.QueryObject = source;
            var sqlParameter = SqlGen.AutoInsert(optionSet, findNestedObjects);
            SqlAccessProvider.Execute(sqlParameter);
            SqlGen.QueryObject = null;
        }
        public void ManualInsert(string tableName, params ISqlProperty[] sqlProperties)
        {
            var sqlParameter = SqlGen.ManualInsert(tableName, sqlProperties);
            SqlAccessProvider.Execute(sqlParameter);
            SqlGen.QueryObject = null;
        }
        public void ManualUpdate(string tableName, string condition, ISqlProperty[] conditionProperties, params ISqlProperty[] sqlProperties)
        {
            var sqlParameter = SqlGen.ManualUpdate(tableName, condition, conditionProperties, sqlProperties);
            SqlAccessProvider.Execute(sqlParameter);
            SqlGen.QueryObject = null;
        }

        // You can leverage nested objects by using these methods.
        public List<T> Query<T>(string sqlStr, int limit = -1, int offset = -1, params ISqlProperty[] sqlProperties)
        {
            var sqlParameter = SqlGen.BuildQuery(sqlStr, limit, offset, sqlProperties);
            return SqlAccessProvider.Query<SqlParameter, T>(sqlParameter);
        }
        public List<T> Query<T>(string sqlStr, object source, int limit = -1, int offset = -1, bool findNestedObjects = true)
        {
            SqlGen.QueryObject = source;
            var sqlParameter = SqlGen.BuildQuery(sqlStr, limit, offset, findNestedObjects);
            var result = SqlAccessProvider.Query<SqlParameter, T>(sqlParameter);
            SqlGen.QueryObject = null;
            return result;
        }
        public int Execute<T>(string sqlStr, params ISqlProperty[] sqlProperties)
        {
            var sqlParameter = SqlGen.GenerateSqlParameter(sqlStr, sqlProperties);
            return SqlAccessProvider.Execute(sqlParameter);
        }
        public int Execute<T>(string sqlStr, object source, bool findNestedObjects = true)
        {
            SqlGen.QueryObject = source;
            var sqlParameter = SqlGen.GenerateSqlParameter(sqlStr, findNestedObjects);
            var result = SqlAccessProvider.Execute(sqlParameter);
            SqlGen.QueryObject = null;
            return result;
        }
    }
}