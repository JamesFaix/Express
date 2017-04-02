using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using System.Reflection;
using System.Linq;

namespace Express.Tests {

    [TestFixture]
    public class ReflectionExtensionTests {

        [Test]
        public void SafeName_ShouldGetCorrectNameofClosedGenericTypes() {
            var type = typeof(Dictionary<int, List<string>>);

            var name = type.SafeName();

            name.ShouldBe("global::System.Collections.Generic.Dictionary<" +
                              "global::System.Int32, " +
                              "global::System.Collections.Generic.List<global::System.String>>");
        }

        [Test]
        public void SafeName_ShouldGetCorrectNameofTypeParameters() {
            var returnType = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Single(t => t.Name == nameof(ReflectionExtensionTests))
                .GetMethods()
                .Single(m => m.Name == nameof(SomeGenericMethod))
                .ReturnType;

            var name = returnType.SafeName();

            name.ShouldBe("T");
        }

        public T SomeGenericMethod<T>(int n) => default(T);

        [Test]
        public void SafeName_ShouldGetCorrectNameOfOpenGenericTypes() {
            var type = typeof(Dictionary<,>);

            var name = type.SafeName();

            name.ShouldBe("global::System.Collections.Generic.Dictionary<TKey, TValue>");
        }        
    }
}
