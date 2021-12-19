using Ao.Lang.Lookup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Ao.Lang.Test.Lookup
{
    [TestClass]
    public class LangLookupBoxTest
    {
        [TestMethod]
        public void InitWithAnyArgs_PropertyValueMustInputs()
        {
            var box = new LangLookupBox("hello");
            Assert.AreEqual("hello", box.Path);
            Assert.IsNull(box.Stream);

            box = new LangLookupBox("hello", Stream.Null, true, true);
            Assert.AreEqual("hello", box.Path);
            Assert.AreEqual(Stream.Null, box.Stream);
            Assert.IsTrue(box.Optional);
            Assert.IsTrue(box.ReloadOnChanged);
            box.ToString();
        }
        [TestMethod]
        public void WhenNoPath_Name_Extensions_LangMustNull()
        {
            var box = new LangLookupBox(null);
            Assert.IsNull(box.Name);
            Assert.IsNull(box.Extension);
            Assert.IsNull(box.GetLangIdentity('.'));
        }
        [TestMethod]
        public void WhenHasPath_Name_Extensions_LangMustActual()
        {
            var path = "C:\\hello.zh-cn.json";
            var box = new LangLookupBox(path);
            Assert.AreEqual("hello.zh-cn.json", box.Name);
            Assert.AreEqual("json", box.Extension);
            Assert.AreEqual("zh-cn", box.GetLangIdentity('.'));
            Assert.AreEqual("zh-cn", box.GetLangIdentity('.', 1));

            path = "C:\\json";
            box = new LangLookupBox(path);
            Assert.IsNull(box.Extension);
        }
        [TestMethod]
        public void GetLangIdentity()
        {
            var path0 = "C:\\dsahjvfdj.hello.zh-cn.json";
            var path1 = "C:\\hello.zh-cn.json";
            var path2 = "C:\\zh-cn.json";
            var path3 = "C:\\.json";

            var box = new LangLookupBox(path0);
            Assert.AreEqual("zh-cn", box.GetLangIdentity('.', 1));
            box = new LangLookupBox(path1);
            Assert.AreEqual("zh-cn", box.GetLangIdentity('.', 1));
            box = new LangLookupBox(path2);
            Assert.AreEqual("zh-cn", box.GetLangIdentity('.', 1));
            box = new LangLookupBox(path3);
            Assert.IsNull(box.GetLangIdentity('.', 1));
        }
    }
}
