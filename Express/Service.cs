using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Express {

    public class Service {

        private readonly IExpressConfig _Config;

        public Service(IExpressConfig config) {
            _Config = config;
        }

        public string GetCode() =>
            new StringBuilder()
                .AppendEach(TypeText, Generator.GetExtensions)
                .ToString();

        internal IEnumerable<string> AssemblyPaths =>
            _Config.AssemblyDirectories
                .SelectMany(p => Directory.GetFiles(p, "*.dll"))
                .Where(AssemblyFilenameFilter);

        private bool AssemblyFilenameFilter(string assemblyPath) =>
            _Config.AssemblyFilter(Path.GetFileName(assemblyPath));

        internal IEnumerable<Type> AllTypes =>
            AssemblyPaths
                .Select(Assembly.LoadFrom)
                .SelectMany(Types)
                .Where(t => t.IsPublic);

        private IEnumerable<Type> Types(Assembly assembly) =>
            assembly.GetTypes()
                .Where(t => _Config.TypeFilter(t)
                         && _Config.NamespaceFilter(t.Namespace));

        internal IEnumerable<TypeText> TypeText =>
            AllTypes
                .Select(t => new TypeText(t, _Config))
                .Where(t => !t.IsEmpty);

    }
}
