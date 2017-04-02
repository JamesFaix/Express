using System.Reflection;

namespace Express {

    class PropertyText {

        public string TypeName { get; }

        public string PropertyTypeName { get; }

        public string PropertyName { get; }

        public PropertyText(PropertyInfo property) {
            TypeName = property.ReflectedType.SafeName();
            PropertyTypeName = property.PropertyType.SafeName();
            PropertyName = property.Name;
        }
    }
}
