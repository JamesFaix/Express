using System;
using System.Collections.Generic;
using System.Linq;

namespace Express {

    class TypeText {

        public Type Type { get; }
        
        public TypeText(Type type, IExpressConfig config) {
            Type = type;

            Properties = type.GetSettableProperties()
                .Where(config.PropertyFilter)
                .Select(p => new PropertyText(p));

            Indexers = type.GetSettableIndexers()
                .Where(config.IndexerFilter)
                .Select(i => new IndexerText(i));

            Methods = type.GetVoidMethods()
                 .Where(config.VoidMethodFilter)
                 .Select(m => new MethodText(m));
        }

        public IEnumerable<PropertyText> Properties { get; }

        public IEnumerable<IndexerText> Indexers { get; }

        public IEnumerable<MethodText> Methods { get; }

        public bool IsEmpty => 
            !Properties.Any() 
            && !Indexers.Any() 
            && !Methods.Any();

        public override string ToString() => 
            $"{Type} {{  }}";
    }
}
