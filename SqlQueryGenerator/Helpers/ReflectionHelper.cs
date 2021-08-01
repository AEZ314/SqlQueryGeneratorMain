using SqlQueryGenerator.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SqlQueryGenerator.Helpers
{
    public static class ReflectionHelper
    {
        public const char MemberAccessChar = '_';

        /// <summary>
        /// Use this for pulling SqlProperties from a source object.
        /// </summary>
        /// <param name="source">The object on which to search for properties with <see cref="SqlPropertyAttribute"/>.</param>
        /// <param name="optionSet">The <see cref="SqlPropertyAttribute.OptionSet"/> set to choose.</param>
        /// <param name="findNestedObjects">Choose whether to search the sub-objects.</param>
        /// <param name="propertyNamePrefix">Passing a value to this parameter is not recommended. This parameter is intended to be automatically used for recursion.</param>
        /// <returns>The list of <see cref="SqlProperty"/> generated from the found properties from the source.</returns>
        public static List<SqlProperty> GetSqlProperties(object source, byte optionSet, bool findNestedObjects, string propertyNamePrefix = "")
        {
            var sqlProperties = new List<SqlProperty>();

            if (source == null) { return sqlProperties; }

            var type = source.GetType();

            var properties = type.GetProperties().Where(x => x.IsDefined(typeof(SqlPropertyAttribute)));
            foreach (var property in properties)
            {
                if (IsPrimitive(property.PropertyType))
                {
                    var attribute = GetOptionAttributeFromProperty<SqlPropertyAttribute>(property, optionSet);
                    sqlProperties.Add((attribute.Column, $"{propertyNamePrefix}{property.Name}", property.GetValue(source)));
                }
                else if (findNestedObjects)
                {
                    var subproperties = GetSqlProperties(property.GetValue(source), optionSet, true, $"{property.Name}{MemberAccessChar}");
                    sqlProperties.AddRange(subproperties);
                }
            }

            return sqlProperties;
        }
        public static List<SqlProperty> GetAnyPropertyNames(object source, bool findNestedObjects, string propertyNamePrefix = "")
        {
            var sqlProperties = new List<SqlProperty>();

            if (source == null) { return sqlProperties; }

            var type = source.GetType();

            var properties = type.GetProperties().Where(x => x.IsDefined(typeof(SqlPropertyAttribute)));
            foreach (var property in properties)
            {
                if (IsPrimitive(property.PropertyType))
                {
                    sqlProperties.Add((null, $"{propertyNamePrefix}{property.Name}", property.GetValue(source)));
                }
                else if (findNestedObjects)
                {
                    var subproperties = GetAnyPropertyNames(property.GetValue(source), true, $"{property.Name}{MemberAccessChar}");
                    sqlProperties.AddRange(subproperties);
                }
            }

            return sqlProperties;
        }
        public static string GetTableName(Type type, byte optionSet)
        {
            return GetOptionAttributeFromType<SqlTableNameAttribute>(type.GetTypeInfo(), optionSet).Table;
        }

        public static T GetOptionAttributeFromProperty<T>(MemberInfo info, byte optionSet) where T : Attribute, IOptionAttribute
        {
            return info.GetCustomAttributes<T>().Where(x => x.OptionSet == optionSet).FirstOrDefault();
        }
        public static T GetOptionAttributeFromType<T>(TypeInfo info, byte optionSet) where T : Attribute, IOptionAttribute
        {
            return info.GetCustomAttributes<T>().Where(x => x.OptionSet == optionSet).FirstOrDefault();
        }

        public static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive || type == typeof(string);
        }
    }
}
