using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;

namespace Express
{

    static class ReflectionExtensions
    {

        private static readonly CSharpCodeProvider _CodeProvider = new CSharpCodeProvider();

        public static string SafeName(this ParameterInfo parameter) =>
            _CodeProvider.IsValidIdentifier(parameter.Name)
                ? parameter.Name
                : "@" + parameter.Name;

        public static string SafeName(this Type type)
        {
            if (type.IsGenericType)
            {
                var t = type.GetGenericTypeDefinition();
                var typeArgs = t.GenericTypeArguments
                    .Select(SafeName)
                    .ToDelimitedString(", ");

                return $"global::{t.FullName.LeftOf('`')}<{typeArgs}>";
            }

            return $"global::{type.FullName}";
        }

        public static IEnumerable<PropertyInfo> GetSettableProperties(this Type type) =>
            type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p =>
                {
                    var setter = p.GetSetMethod();
                    return setter != null
                        && setter.GetParameters().Length == 1;
                });

        public static IEnumerable<PropertyInfo> GetSettableIndexers(this Type type) =>
            type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p =>
                {
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
