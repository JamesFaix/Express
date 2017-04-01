using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Express {
    public class Generator {

        private readonly IExpressConfig _Config;

        public Generator(IExpressConfig config) {
            _Config = config;
        }

        public string GetCode() =>
            new StringBuilder()
                .AppendEach(TypeProperties, GetExtensions)
                .ToString();

        internal IEnumerable<string> AssemblyPaths =>
            _Config.AssemblyDirectories
                .SelectMany(p => Directory.GetFiles(p, "*.dll"))
                .Where(AssemblyFilenameFilter);

        private bool AssemblyFilenameFilter(string assemblyPath) =>
            _Config.AssemblyFilter(Path.GetFileName(assemblyPath));

        internal IEnumerable<TypeProperties> TypeProperties =>
            AssemblyPaths
                .Select(Assembly.LoadFrom)
                .SelectMany(GetTypeProperties)
                .Where(tp => tp.Properties.Any());

        internal IEnumerable<TypeProperties> GetTypeProperties(Assembly assembly) =>
            assembly.GetTypes()
                .Where(t => _Config.TypeFilter(t)
                         && _Config.NamespaceFilter(t.Namespace))
                .Select(GetTypeProperties);

        internal TypeProperties GetTypeProperties(Type type) =>
            new TypeProperties(type,
                type.GetSettableProperties()
                    .Where(_Config.PropertyFilter));

        //TODO: Add method for indexers

        static string GetExtensions(TypeProperties typeProperties) =>
            new StringBuilder()
                .AppendLine($"\t\t#region {typeProperties.Type.FullName}")
                .AppendEach(typeProperties.Properties, GetWithExtension)
                .AppendLine()
                .AppendLine("\t\t#endregion")
                .AppendLine()
                .ToString();

        static string GetWithExtension(PropertyInfo info) => 
            GetWithExtension(new PropertyText(info));

        static string GetWithExtension(PropertyText text) => $@"
        public static {text.TypeName} With{text.PropertyName}(
            this {text.TypeName} @this, {text.PropertyTypeName} value) 
        {{
            @this.{text.PropertyName} = value;
            return @this;
        }}
";
    }
}
