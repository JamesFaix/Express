using System;
using System.Collections.Generic;
using System.Reflection;

namespace Express {
    
    public interface IExpressConfig {

        IEnumerable<string> AssemblyDirectories { get; }

        bool AssemblyFilter(string assemblyName);

        bool NamespaceFilter(string @namespace);

        bool TypeFilter(Type type);

        bool PropertyFilter(PropertyInfo property);

    }
}
