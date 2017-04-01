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
            type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => {
                    var setter = p.GetSetMethod();
                    return setter != null
                        && setter.GetParameters().Length == 1;
                });

        public static IEnumerable<PropertyInfo> GetSettableIndexers(this Type type) =>
            type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => {
                    var setter = p.GetSetMethod();
                    return setter != null
                        && setter.GetParameters().Length > 1;
                });

        public static IEnumerable<MethodInfo> GetVoidMethods(this Type type) =>
            type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.ReturnType == typeof(void)
                        && !m.IsSpecialName);
    }
}
