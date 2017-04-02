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

        public string TypeArguments { get; }

        public MethodText(MethodInfo info) {
            TypeName = info.ReflectedType.SafeName();
            MethodName = info.Name;
            ReturnTypeName = info.ReturnType.SafeName();

            var parameters = info.GetParameters();

            ParameterList = parameters
                .Select(p => $"{p.SafeName()}")
                .ToDelimitedString(", ");

            ParameterListWithTypes = parameters
                .Select(p => $"{p.ParameterType.SafeName()} {p.SafeName()}")
                .ToDelimitedString(", ");

            TypeArguments = info.IsGenericMethod
                ? $"<{info.GetGenericArguments().Select(t => t.SafeName()).ToDelimitedString(", ")}>"
                : "";
        }
    }
}
