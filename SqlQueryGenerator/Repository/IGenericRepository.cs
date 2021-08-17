using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Repository
{
    public interface IGenericRepository
    {
        
        
        void AutoInsert(object source, byte optionSet = 0, bool findNestedObjects = true);
        void ManualInsert(string tableName, params ISqlProperty[] sqlProperties);
        void ManualUpdate(string tableName, string condition, ISqlProperty[] conditionProperties, params ISqlProperty[] sqlProperties);

        List<T> Query<T>(string sqlStr, int limit = -1, int offset = -1, params ISqlProperty[] sqlProperties);
        List<T> Query<T>(string sqlStr, object source, int limit = -1, int offset = -1, bool findNestedObjects = true);
        int Execute<T>(string sqlStr, params ISqlProperty[] sqlProperties);
        int Execute<T>(string sqlStr, object source, bool findNestedObjects = true);
    }
}
