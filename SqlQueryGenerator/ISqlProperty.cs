using System;
using System.Collections.Generic;
using System.Text;

namespace SqlQueryGenerator
{
    public interface ISqlProperty
    {
        /// <summary>
        /// Name of the associated DB column.
        /// </summary>
        string Column { get; set; }
        /// <summary>
        /// Name of the property.
        /// </summary>
        string Property { get; set; }
        /// <summary>
        /// Value of the property.
        /// </summary>
        object Value { get; set; }
    }
}
