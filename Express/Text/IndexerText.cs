using System;
using System.Linq;
using System.Reflection;

namespace Express.Text {

    class IndexerText : IMemberText {

        public string ExtendedType { get; }

        public string TypeParameters { get; }

        public string MemberName { get; }

        public string PropertyType { get; }

        public string ParameterList { get; }

        public string ParameterListWithTypes { get; }

        public IndexerText(PropertyInfo indexer) {
            var type = indexer.ReflectedType;

            ExtendedType = type.SafeName();
            TypeParameters = ((TypeInfo)type).GenericTypeParameters.ToGenericParameterList();
            MemberName = "Item";
            PropertyType = indexer.PropertyType.SafeName();

            var parameters = indexer.SetMethod.GetParameters();
            var indexes = parameters.Take(parameters.Count() - 1);

            ParameterList = indexes
                .Select(p => $"{p.SafeName()}")
                .ToDelimitedString(", ");

            ParameterListWithTypes = indexes
                .Select(p => $"{p.ParameterType.SafeName()} {p.SafeName()}")
                .ToDelimitedString(", ");
        }
    }
}
