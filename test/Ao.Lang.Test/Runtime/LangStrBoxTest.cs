using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;

namespace Ao.Lang.Runtime.Test
{
    [TestClass]
    public class LangStrBoxTest
    {
        [TestMethod]
        public void GivenValue_ChangeMustRaise()
        {
            var box = new LangStrBox();
            string name = null;
            object raiseBox = null;
            box.PropertyChanged += (o, e) =>
            {
                name = e.PropertyName;
                raiseBox = o;
            };
            var val = "hello";
            box.Value = val;
            Assert.AreEqual(val, box.Value);
            Assert.AreEqual(nameof(LangStrBox.Value), name);
            Assert.AreEqual(box, raiseBox);
        }
        //https://github.com/Cricle/Ao.Lang/issues/8
        [TestMethod]
        public void Github_8_Null_To_Value() 
        {
            var mgr = LanguageManager.Instance;
            mgr.LangService.EnsureGetLangNode("zh-cn")
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["title"] = "标题"
                });
            mgr.LangService.EnsureGetLangNode("en-us")
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["title"] = "Title"
                });

            mgr.CultureInfo = new CultureInfo("fr");
            var box = mgr.CreateLangBox("title",noUpdate:true);
            Assert.IsNull(box.Value);
            mgr.CultureInfo = new CultureInfo("zh-cn");
            Assert.AreEqual("标题", box.Value);
            mgr.CultureInfo = new CultureInfo("en-us");
            Assert.AreEqual("标题", box.Value);
        }
        [TestMethod]
        public void Github_8_Value_To_Value()
        {
            var mgr = LanguageManager.Instance;
            mgr.LangService.EnsureGetLangNode("zh-cn")
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["title"] = "标题"
                });
            mgr.LangService.EnsureGetLangNode("en-us")
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["title"] = "Title"
                });

            mgr.CultureInfo = new CultureInfo("en-us");
            var box = mgr.CreateLangBox("title", noUpdate: true);
            Assert.AreEqual("Title", box.Value);
            mgr.CultureInfo = new CultureInfo("zh-cn");
            Assert.AreEqual("Title", box.Value);
        }
    }
}
