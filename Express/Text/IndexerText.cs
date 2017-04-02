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
            TypeName = indexer.ReflectedType.SafeName();
            PropertyTypeName = indexer.PropertyType.SafeName();

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
