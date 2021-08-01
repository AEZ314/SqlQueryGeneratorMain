using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Repository
{
    public interface IGenericRepository
    {
        
        
        void AutoInsert(object source, byte optionSet = 0, bool findNestedObjects = true);
        void ManualInsert<T>(string tableName, params T[] sqlProperties) where T : ISqlProperty;

        List<J> Query<T, J>(string sqlStr, int limit = -1, int offset = -1, params T[] sqlProperties) where T : ISqlProperty;
        List<J> Query<J>(string sqlStr, object source, int limit = -1, int offset = -1, bool findNestedObjects = true);
        int Execute<T, J>(string sqlStr, params T[] sqlProperties) where T : ISqlProperty;
        int Execute<J>(string sqlStr, object source, bool findNestedObjects = true);
    }
}
