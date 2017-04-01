using System;
using System.Collections.Generic;
using System.Reflection;

namespace Express.Tests {

    public class Config : IExpressConfig {

        public static Config Instance { get; } = new Config();

        public IEnumerable<string> AssemblyDirectories =>
            new[] {
                @"C:\Git\Express\Express.Demo\bin\Debug\"
            };

        public bool AssemblyFilter(string assemblyName) =>
            !assemblyName.StartsWith("System") &&
            !assemblyName.StartsWith("Microsoft") &&
            !assemblyName.StartsWith("nunit") &&
            !assemblyName.StartsWith("Shouldly");

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
