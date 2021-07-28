using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator.Repository
{
    public interface IGenericRepository
    {
        void ManualInsert<T>(string tableName, object source, params T[] sqlProperties) where T : ISqlProperty;
        void AutoInsert(string tableName, object source, byte optionSet, bool useNestedObjects);


    }
}
