using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Express {

    static class ReflectionExtensions {

        public static string FullyQualifiedName(this Type type) {
            var typeArgs = type.GenericTypeArguments
                .Select(FullyQualifiedName)
                .ToDelimitedString(", ");

            return type.IsGenericType
                ? $"global::{type.FullName.LeftOf('`')}<{typeArgs}>"
                : $"global::{type.FullName}";
        }

        public static IEnumerable<PropertyInfo> GetSettableProperties(this Type type) =>
            type.GetProperties()
                .Where(p => {
                    var setter = p.GetSetMethod();
                    return setter != null
                        && setter.IsPublic
                        && !setter.IsStatic
                        && setter.GetParameters().Length == 1;
                });

    
        //TODO: Add method for indexers
    }
}
