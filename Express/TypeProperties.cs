using System;
using System.Collections.Generic;
using System.Reflection;

namespace Express {

    class TypeProperties {

        public Type Type { get; }

        public IEnumerable<PropertyInfo> Properties { get; }

        public TypeProperties(Type type, IEnumerable<PropertyInfo> properties) {
            Type = type;
            Properties = properties;
        }

        public override string ToString() => 
            $"{Type} {{ {Properties.ToDelimitedString(", ")} }}";
    }
}
