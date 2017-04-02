using System.Linq;
using System.Reflection;
using System;
using System.Collections.Generic;

namespace Express.Text {

    class MethodText : IMemberText {

        public string ExtendedType { get; }

        public string TypeParameters { get; }

        public string MemberName { get; }

        public string MemberTypeParameters { get; }

        public string ReturnTypeName { get; }

        public string ParameterList { get; }

        public string ParameterListWithTypes { get; }

        public MethodText(MethodInfo info) {
            var type = info.ReflectedType;

            ExtendedType = type.SafeName();

            var methodTypeParameters = info.GetGenericArguments();
            var classTypeParameters = ((TypeInfo)type).GenericTypeParameters;
            MemberTypeParameters = methodTypeParameters.ToGenericParameterList();
            TypeParameters = classTypeParameters.Concat(methodTypeParameters).ToGenericParameterList();

            MemberName = info.Name;
            ReturnTypeName = info.ReturnType.SafeName();

            var parameters = info.GetParameters();

            ParameterList = parameters
                .Select(p => $"{p.SafeName()}")
                .ToDelimitedString(", ");

            ParameterListWithTypes = parameters
                .Select(p => $"{p.ParameterType.SafeName()} {p.SafeName()}")
                .ToDelimitedString(", ");
        }
    }
}
