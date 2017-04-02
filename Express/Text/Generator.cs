using System.Text;

namespace Express
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
        public static {text.TypeName} Set{text.PropertyName}(
            this {text.TypeName} @this, {text.PropertyTypeName} value) 
        {{
            @this.{text.PropertyName} = value;
            return @this;
        }}
";

        static string GetSetIndexExtension(IndexerText text) => $@"
        public static {text.TypeName} SetItem(
            this {text.TypeName} @this, {text.ParameterListWithTypes}, {text.PropertyTypeName} value) 
        {{
            @this[{text.ParameterList}] = value;
            return @this;
        }}
";

        static string GetDoMethodExtension(MethodText text) => $@"
        public static {text.TypeName} Do{text.MethodName}{text.TypeArguments}(
            this {text.TypeName} @this{(text.ParameterListWithTypes.Length > 0 ? ", " : "")}{text.ParameterListWithTypes}) 
        {{
            @this.{text.MethodName}({text.ParameterList});
            return @this;
        }}
";
    }
}
