using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace Express.Tests
{

    [TestFixture]
    public class GeneratorTests
    {

        [Test]
        public void AssemblyPaths_ShouldReturnCorrectPaths()
        {
            var assemblyNames = new Service(Config.Instance)
                .AssemblyPaths
                .Select(Path.GetFileName)
                .ToArray();

            assemblyNames.Length.ShouldBe(2);
            assemblyNames[0].ShouldBe(@"Express.Demo.dll");
            assemblyNames[1].ShouldBe(@"Express.dll");
        }

        [Test]
        public void TypeProperties_ShouldReturnCorrectProperties()
        {
            var typeProperties = new Service(Config.Instance)
                .TypeText
                .ToArray();

            typeProperties.Length.ShouldBe(2);

            var dogType = typeProperties.Single(tp => tp.Type.Name == "Dog");
            dogType.Properties.Count().ShouldBe(3);

            var blackBoxType = typeProperties.Single(tp => tp.Type.Name == "BlackBox");
            blackBoxType.Properties.Count().ShouldBe(2);
        }
    }
}
