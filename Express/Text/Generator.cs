using System.Text;

namespace Express.Text
{
    internal static class Generator
    {
        public static string GetExtensions(TypeText type) =>
            new StringBuilder()
                .AppendLine($"\t\t#region {type.Type.SafeName()}")
                .AppendEach(type.Properties, GetSetPropertyExtension)
                .AppendEach(type.Indexers, GetSetIndexExtension)
                .AppendEach(type.Methods, GetDoMethodExtension)
                .AppendLine()
                .AppendLine("\t\t#endregion")
                .AppendLine()
                .ToString();

        static string GetSetPropertyExtension(PropertyText text) => $@"
        public static {text.ExtendedType} Set{text.MemberName}{text.TypeParameters}(
            this {text.ExtendedType} @this, {text.MemberType} value) 
        {{
            @this.{text.MemberName} = value;
            return @this;
        }}
";

        static string GetSetIndexExtension(IndexerText text) => $@"
        public static {text.ExtendedType} SetItem{text.TypeParameters}(
            this {text.ExtendedType} @this, {text.ParameterListWithTypes}, {text.PropertyType} value) 
        {{
            @this[{text.ParameterList}] = value;
            return @this;
        }}
";

        static string GetDoMethodExtension(MethodText text) => $@"
        public static {text.ExtendedType} Do{text.MemberName}{text.TypeParameters}(
            this {text.ExtendedType} @this{(text.ParameterListWithTypes.Length > 0 ? ", " : "")}{text.ParameterListWithTypes}) 
        {{
            @this.{text.MemberName}{text.MemberTypeParameters}({text.ParameterList});
            return @this;
        }}
";
    }
}
