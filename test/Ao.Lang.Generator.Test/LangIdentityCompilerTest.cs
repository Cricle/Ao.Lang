using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Ao.Lang.Generator.Test
{
    [TestClass]
    public class LangIdentityCompilerTest
    {
        [TestMethod]
        public void GetTwiceDefault_MustEqual()
        {
            var a = LangIdentityCompiler.Default;
            var b = LangIdentityCompiler.Default;
            Assert.AreEqual(a, b);
        }
        [TestMethod]
        public void GivenNullInitOrCall_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new LangIdentityCompiler(null));
            Assert.ThrowsException<ArgumentNullException>(() => new LangIdentityCompiler("-").Compile(null));
        }
        class ValueLangIdentity : ILangIdentity
        {

            public string[] Blocks { get; set; }

            public string[] GetIdentityBlocks()
            {
                return Blocks;
            }
        }
        [TestMethod]
        public void GivenIdentityCompile_MustCompiled()
        {
            var compiler = new LangIdentityCompiler("-");
            var blocks = new string[]
            {
                "dsgafgak",
                "hello",
                "world"
            };
            var identity = new ValueLangIdentity { Blocks = blocks };
            var actual = compiler.Compile(identity);
            var exp = string.Join("-", blocks);
            Assert.AreEqual(exp, actual);
        }
    }
}
