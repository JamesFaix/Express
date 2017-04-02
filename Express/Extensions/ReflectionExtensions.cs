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

        public static string SafeName(this Type type) {
            if (type.IsConstructedGenericType) {
                var typeArgs = type.GenericTypeArguments
                    .Select(SafeName)
                    .ToDelimitedString(", ");

                return $"global::{type.FullName.LeftOf('`')}<{typeArgs}>";
            }
            else if (type.IsGenericType) {
                var typeParams = ((TypeInfo)type).GenericTypeParameters
                    .Select(SafeName)
                    .ToDelimitedString(", ");

                return $"global::{type.FullName.LeftOf('`')}<{typeParams}>";
            }
            else if (type.IsGenericParameter) {
                return type.Name;
            }
            else {
                return $"global::{type.FullName}";
            }
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

        public static string ToGenericParameterList(this IEnumerable<Type> types) =>
            types.Any()
                ? $"<{types.Select(ReflectionExtensions.SafeName).ToDelimitedString(", ")}>"
                : "";
    }
}
