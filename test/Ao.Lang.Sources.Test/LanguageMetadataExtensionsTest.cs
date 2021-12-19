using Ao.Lang.Lookup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Ao.Lang.Sources.Test
{
    [TestClass]
    public class LanguageMetadataExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowExcpetion()
        {
            Assert.ThrowsException<ArgumentNullException>(() => LanguageMetadataExtensions.EnableAll(null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageMetadataExtensions.EnableIni(null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageMetadataExtensions.EnableJson(null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageMetadataExtensions.EnableXml(null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageMetadataExtensions.EnableYaml(null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageMetadataExtensions.EnableResources(null));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageMetadataExtensions.EnableResx(null));

            var ass = typeof(LanguageMetadataExtensionsTest).Assembly;
            var langSer = LanguageService.Default;

            Assert.ThrowsException<ArgumentNullException>(() => LanguageMetadataExtensions.RaiseAssemblyResources(null, ass, 1));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageMetadataExtensions.RaiseAssemblyResources(langSer, null, 1));
            Assert.ThrowsException<ArgumentNullException>(() => LanguageMetadataExtensions.RaiseAssemblyResources<object>(null, 2));
        }
        [TestMethod]
        [DataRow("json")]
        [DataRow("ini")]
        [DataRow("xml")]
        [DataRow("resx")]
        [DataRow("resources")]
        [DataRow("yaml")]
        public void EnableAll_Must(string extensions)
        {
            var lookup = new LangLookup(new LanguageService());

            var fi = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "a.zh-cn." + extensions);

            LanguageMetadataExtensions.EnableAll(lookup);

            var ok = lookup.Raise(fi, true, false);
            Assert.IsTrue(ok);
        }
        [TestMethod]
        public void AddAssemblyResources()
        {
            var langSer = new LanguageService();
            LanguageMetadataExtensions.RaiseAssemblyResources(langSer, GetType().Assembly, 2);

            Assert.IsNotNull(langSer.GetRoot("zh-cn"));
            Assert.IsNotNull(langSer.GetRoot("en-us"));

            Assert.IsNull(langSer.GetRoot("fr"));

            langSer = new LanguageService();
            LanguageMetadataExtensions.RaiseAssemblyResources<LanguageMetadataExtensionsTest>(langSer, 2);

            Assert.IsNotNull(langSer.GetRoot("zh-cn"));
            Assert.IsNotNull(langSer.GetRoot("en-us"));

            Assert.IsNull(langSer.GetRoot("fr"));
        }
    }
}
