using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System.Threading;

namespace Ao.Lang.Test
{
    [TestClass]
    public class LanguageServiceTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            var ser = new LanguageService();
            Assert.ThrowsException<ArgumentNullException>(() => ser.Add(null));
            Assert.ThrowsException<ArgumentNullException>(() => ser.CultureIsSupport(null));
            Assert.ThrowsException<ArgumentNullException>(() => ser.EnsureGetLangNode(null));
            Assert.ThrowsException<ArgumentNullException>(() => ser.GetLangNode(null));
            Assert.ThrowsException<ArgumentNullException>(() => ser.GetRoot(null));
            Assert.ThrowsException<ArgumentNullException>(() => ser.IsBuilt(null));
            Assert.ThrowsException<ArgumentNullException>(() => ser.Remove(null));
        }
        [TestMethod]
        public void AddMedata_RemoveIt_MustRemoved()
        {
            var culture = new CultureInfo("zh-cn");
            var ser = new LanguageService();
            var m = new DefaultLanguageMetadata(culture);
            ser.Add(m);
            Assert.AreEqual(1, ser.Count);
            Assert.AreEqual(m, ser.Single());
            ser.Remove(m);
            Assert.AreEqual(0, ser.Count);
            Assert.IsFalse(ser.Any());
        }
        [TestMethod]
        public void AddMedata_ClearIt_MustHasNothing()
        {
            var culture = new CultureInfo("zh-cn");
            var ser = new LanguageService();
            var m = new DefaultLanguageMetadata(culture);
            ser.Add(m);
            ser.Add(m);
            ser.Clear();
            Assert.AreEqual(0, ser.Count);
            Assert.IsFalse(((IEnumerable)ser).OfType<ILanguageMetadata>().Any());
            Assert.AreEqual(0, ser.SupportCultures.Count);
        }
        [TestMethod]
        public void AddMedata_GetRoot_MustGot()
        {
            var culture = new CultureInfo("en-us");
            var ser = new LanguageService();
            var node = ser.GetLangNode(culture);
            Assert.IsNull(node);
            node=ser.EnsureGetLangNode(culture);
            node.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["Title"] = "title",
                ["Name"] = "name"
            });
            var root = ser.GetLangNode(culture);
            Assert.IsNotNull(root);
            var title = root.Root["Title"];
            Assert.AreEqual("title", title);

            var node2 = ser.EnsureGetLangNode(culture);
            Assert.AreEqual(node, node2);
        }
        [TestMethod]
        public void ReBuild_AllMedataMustRebuilt()
        {
            var culture = new CultureInfo("zh-cn");
#if NET452
            Thread.CurrentThread.CurrentCulture = culture;
#else
            CultureInfo.CurrentCulture =culture;
#endif
            var ser = new LanguageService();
            var m = new DefaultLanguageMetadata(culture);
            ser.Add(m);

            var root = ser.GetRoot(culture);
            var rootx = ser.GetRoot(culture);
            Assert.IsTrue(root== rootx);
            ser.ReBuild();
            var root2 = ser.GetRoot(culture);
            Assert.IsFalse(root== root2);
        }
        [TestMethod]
        public void ReBuildOnChangedIsSyncMedatas()
        {
            var culture = new CultureInfo("zh-cn");
            var ser = new LanguageService();
            var m = new DefaultLanguageMetadata(culture);
            ser.Add(m);
            var node = ser.GetLangNode(culture);
            ser.ReBuildIfCollectionChanged = false;
            Assert.IsFalse(node.ReBuildIfCollectionChanged);
            ser.ReBuildIfCollectionChanged = true;
            Assert.IsTrue(node.ReBuildIfCollectionChanged);
        }
        [TestMethod]
        public void CultureIsSupport()
        {
            var culture = new CultureInfo("zh-cn");
            var ser = new LanguageService();
            Assert.IsFalse(ser.CultureIsSupport(culture));
            var m = new DefaultLanguageMetadata(culture);
            ser.Add(m);
            Assert.IsTrue(ser.CultureIsSupport(culture));
        }
        [TestMethod]
        public void IsBuild()
        {
            var culture = new CultureInfo("zh-cn");
            var ser = new LanguageService();
            var m = new DefaultLanguageMetadata(culture);
            ser.Add(m);
            Assert.IsFalse(ser.IsBuilt(culture));
            var root=ser.GetRoot(culture);
            Assert.IsTrue(ser.IsBuilt(culture));
        }
        [TestMethod]
        public void GetRoot()
        {
            var culture = new CultureInfo("zh-cn");
            var ser = new LanguageService();
            Assert.IsNull(ser.GetRoot(culture));
            var m = new DefaultLanguageMetadata(culture);
            ser.Add(m);
            Assert.IsNotNull(ser.GetRoot(culture));
        }
        [TestMethod]
        public void ReBuildIfCollectionChangedValueChanged()
        {
            var ser = new LanguageService();
            LanguageService nser = null;
            bool value=false;
            ser.ReBuildIfCollectionChangedValueChanged += (o, e) =>
            {
                nser = o;
                value = e;
            };
            ser.ReBuildIfCollectionChanged = true;
            Assert.IsTrue(ser.ReBuildIfCollectionChanged);
            Assert.AreEqual(ser, nser);
            Assert.IsTrue(value);
        }
        [TestMethod]
        public void ThisGetValue()
        {
            var culture = new CultureInfo("en-us");
#if NET452
            Thread.CurrentThread.CurrentCulture = culture;
#else
            CultureInfo.CurrentCulture =culture;
#endif
            var ser = new LanguageService();
            var node = ser.EnsureGetLangNode(culture);
            node.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["Title"] = "title",
                ["Name"] = "name"
            });
            var title = ser["Title"];
            Assert.AreEqual("title", title);

            var nothing = ser["a"];
            Assert.IsNull(nothing);
        }
        [TestMethod]
        public void ThisGetValue_Format()
        {
            var culture = new CultureInfo("en-us");
#if NET452
            Thread.CurrentThread.CurrentCulture = culture;
#else
            CultureInfo.CurrentCulture =culture;
#endif
            var ser = new LanguageService();
            var node = ser.EnsureGetLangNode(culture);
            node.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["Title"] = "title",
                ["Name"] = "name",
                ["Format"]="hello{0}world"
            });
            var title = ser["Format","a"];
            Assert.AreEqual("helloaworld", title);

            var nothing = ser["q","a"];
            Assert.IsNull(nothing);
        }
    }
}
