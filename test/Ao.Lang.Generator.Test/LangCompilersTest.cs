using Ao.Lang.Generator.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Lang.Generator.Test
{
    [TestClass]
    public class LangCompilersTest
    {
        private readonly Dictionary<LangIdentity,string> datas= new Dictionary<LangIdentity, string>
        {
            [new LangIdentity()] = "a",
            [new LangIdentity { C = "a" }] = "b"
        };
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var langIdentity = LangIdentityCompiler.Default;

            Assert.ThrowsException<ArgumentNullException>(() => JsonLangCompilers.CompileJson<LangIdentity>(langIdentity, null));
            Assert.ThrowsException<ArgumentNullException>(() => JsonLangCompilers.CompileJson<LangIdentity>(null, datas));

            Assert.ThrowsException<ArgumentNullException>(() => ResourcesLangCompilers.CompileResource<LangIdentity>(langIdentity, null));
            Assert.ThrowsException<ArgumentNullException>(() => ResourcesLangCompilers.CompileResource<LangIdentity>(null, datas));
        }
        [TestMethod]
        public void CompileJson()
        {
            var langIdentity = LangIdentityCompiler.Default;
            var str = JsonLangCompilers.CompileJson<LangIdentity>(langIdentity, datas);
            Assert.IsFalse(string.IsNullOrEmpty(str));
        }
        [TestMethod]
        public void CompileResx()
        {
            var langIdentity = LangIdentityCompiler.Default;
            var stream = ResourcesLangCompilers.CompileResource<LangIdentity>(langIdentity, datas);
            Assert.IsNotNull(stream);
            Assert.AreNotEqual(0, stream.Length);
        }
    }
}
