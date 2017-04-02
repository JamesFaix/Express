using System.Reflection;

namespace Express.Text {

    class PropertyText : IMemberText {

        public string ExtendedType { get; }

        public string TypeParameters { get; }

        public string MemberName { get; }

        public string MemberType { get; }

        public PropertyText(PropertyInfo property) {
            var type = property.ReflectedType;

            ExtendedType = type.SafeName();
            TypeParameters = ((TypeInfo)type).GenericTypeParameters.ToGenericParameterList();
            MemberName = property.Name;
            MemberType = property.PropertyType.SafeName();
        }
    }
}
