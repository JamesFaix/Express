using System;
using System.Linq;
using System.Reflection;

namespace Express {

    class MethodText {

        public string TypeName { get; }

        public string MethodName { get; }

        public string ReturnTypeName { get; }

        public string ParameterList { get; }

        public string ParameterListWithTypes { get; }

        public MethodText(MethodInfo info) {
            TypeName = info.ReflectedType.FullyQualifiedName();
            MethodName = info.Name;
            ReturnTypeName = info.ReturnType.FullyQualifiedName();

            var parameters = info.GetParameters();

            ParameterList = parameters
                .Select(p => $"{p.Name}")
                .ToDelimitedString(", ");

            ParameterListWithTypes = parameters
                .Select(p => $"{p.ParameterType.FullyQualifiedName()} {p.Name}")
                .ToDelimitedString(", ");
        }
    }
}
