using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private SqlGen _SqlGen;

        public SqlGen SqlGen => _SqlGen ?? (_SqlGen = new SqlGen());
        public SqlQueryProvider SqlQueryProvider { get; set; }

        public GenericRepository(SqlQueryProvider sqlAccessProvider)
        {
            SqlQueryProvider = sqlAccessProvider;
        }
        
        
        public void ManualInsert<T>(string tableName, object source, params T[] sqlProperties) where T : ISqlProperty
        {
            SqlGen.QueryObject = source;
            var sqlParameter = SqlGen.ManualInsert(tableName, sqlProperties);
            SqlQueryProvider.Execute(sqlParameter);
            SqlGen.QueryObject = null;
        }

        public void AutoInsert(string tableName, object source, byte optionSet = 0, bool useNestedObjects = true)
        {
            SqlGen.QueryObject = source;
            var sqlParameter = SqlGen.AutoInsert(tableName, optionSet, useNestedObjects);
            SqlQueryProvider.Execute(sqlParameter);
            SqlGen.QueryObject = null;
        }
    }
}
