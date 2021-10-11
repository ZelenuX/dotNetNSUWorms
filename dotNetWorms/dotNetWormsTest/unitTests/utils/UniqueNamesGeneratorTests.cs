using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Utils.Generators;

namespace dotNetWormsTest.unitTests.utils
{
    public class UniqueNamesGeneratorTests
    {
        [Fact]
        public void NameUniquienessTest()
        {
            int iterations = 1000;
            var generator = new UniqueNamesGenerator("BobTheWorm_");
            var names = new HashSet<string>();

            for (int i = 0; i < iterations; ++i)
            {
                names.Add(generator.generate());
            }

            Assert.Equal(iterations, names.Count);
            var enumerator = names.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Assert.Matches("BobTheWorm_*", enumerator.Current);
            }
        }
    }
}
