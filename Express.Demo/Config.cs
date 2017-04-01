using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Express.Demo {

    public class Config : IExpressConfig {

        public static Config Instance { get; } = new Config();

        public IEnumerable<string> AssemblyDirectories =>
            new[] {
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
            };

        public bool AssemblyFilter(string assemblyName) =>
            !assemblyName.StartsWith("System") &&
            !assemblyName.StartsWith("Microsoft") &&
            !assemblyName.StartsWith("nunit") &&
            !assemblyName.StartsWith("Shouldly") &&
            assemblyName != "WithIt.Tests.dll";

        public bool NamespaceFilter(string @namespace) =>
            true;

        public bool TypeFilter(Type type) =>
            true;

        public bool PropertyFilter(PropertyInfo property) =>
            true;

        public bool IndexerFilter(PropertyInfo indexer) =>
            true;

        public bool VoidMethodFilter(MethodInfo method) =>
            true;
    }
}
