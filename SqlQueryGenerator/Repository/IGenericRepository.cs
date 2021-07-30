using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Repository
{
    public interface IGenericRepository
    {
        List<J> Query<T, J>(string sqlStr, params T[] sqlProperties) where T : ISqlProperty;
        List<J> Query<J>(string sqlStr, object source, bool findNestedObjects);
        int Execute<T, J>(string sqlStr, params T[] sqlProperties) where T : ISqlProperty;
        int Execute<J>(string sqlStr, object source, bool findNestedObjects);

        void ManualInsert<T>(string tableName, params T[] sqlProperties) where T : ISqlProperty;
        void AutoInsert(object source, byte optionSet, bool findNestedObjects);
    }
}
