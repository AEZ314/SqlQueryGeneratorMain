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
        
        // These methods are to be used when the other methods aren't enough. *You can leverage nested objects by using these methods.
        public List<J> Query<T, J>(string sqlStr, params T[] sqlProperties) where T : ISqlProperty
        {
            var sqlParameter = SqlGen.GenerateSqlParameter(sqlStr, sqlProperties);
            return SqlAccessProvider.Query<SqlParameter, J>(sqlParameter);
        }
        public List<J> Query<J>(string sqlStr, object source, bool findNestedObjects = true)
        {
            SqlGen.QueryObject = source;
            var sqlParameter = SqlGen.GenerateSqlParameter(sqlStr, findNestedObjects);
            var result = SqlAccessProvider.Query<SqlParameter, J>(sqlParameter);
            SqlGen.QueryObject = null;
            return result;
        }
        public int Execute<T, J>(string sqlStr, params T[] sqlProperties) where T : ISqlProperty
        {
            var sqlParameter = SqlGen.GenerateSqlParameter(sqlStr, sqlProperties);
            return SqlAccessProvider.Execute(sqlParameter);
        }
        public int Execute<J>(string sqlStr, object source, bool findNestedObjects = true)
        {
            SqlGen.QueryObject = source;
            var sqlParameter = SqlGen.GenerateSqlParameter(sqlStr, findNestedObjects);
            var result = SqlAccessProvider.Execute(sqlParameter);
            SqlGen.QueryObject = null;
            return result;
        }

        public void ManualInsert<T>(string tableName, params T[] sqlProperties) where T : ISqlProperty
        {
            var sqlParameter = SqlGen.ManualInsert(tableName, sqlProperties);
            SqlAccessProvider.Execute(sqlParameter);
            SqlGen.QueryObject = null;
        }
        public void AutoInsert(object source, byte optionSet = 0, bool findNestedObjects = true)
        {
            SqlGen.QueryObject = source;
            var sqlParameter = SqlGen.AutoInsert(optionSet, findNestedObjects);
            SqlAccessProvider.Execute(sqlParameter);
            SqlGen.QueryObject = null;
        }



    }
}
