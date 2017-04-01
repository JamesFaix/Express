using System;
using System.Linq;
using System.Reflection;

namespace Express {

    class IndexerText {

        public string TypeName { get; }

        public string PropertyTypeName { get; }

        public string ParameterList { get; }

        public string ParameterListWithTypes { get; }

        public IndexerText(PropertyInfo indexer) {
            TypeName = indexer.ReflectedType.FullyQualifiedName();
            PropertyTypeName = indexer.PropertyType.FullyQualifiedName();

            var parameters = indexer.SetMethod.GetParameters();
            var indexes = parameters.Take(parameters.Count() - 1);

            ParameterList = indexes
                .Select(p => $"{p.Name}")
                .ToDelimitedString(", ");

            ParameterListWithTypes = indexes
                .Select(p => $"{p.ParameterType.FullyQualifiedName()} {p.Name}")
                .ToDelimitedString(", ");
        }
    }
}
