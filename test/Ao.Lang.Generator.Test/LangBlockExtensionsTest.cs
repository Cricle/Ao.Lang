using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Lang.Generator.Test
{
    [TestClass]
    public class LangBlockExtensionsTest
    {
        [TestMethod]
        public void GivenNullCall_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => LangBlockExtensions.ToCultureMap<LangBlock>(null));
        }
        [TestMethod]
        public void GivenAnyLangs_ToCultureMap_MustContainsThem()
        {
            var block = new LangBlock
            {
                C = "i",
                CultureStringMapping =
                {
                    ["zh-cn"]="我",
                    ["en-us"]="me"
                }
            };
            var map = LangBlockExtensions.ToCultureMap<LangBlock>(new LangBlock[] { block,block});
            Assert.AreEqual(2,map.Count);
            Assert.IsTrue(map.ContainsKey(new CultureInfo("zh-cn")));
            Assert.IsTrue(map.ContainsKey(new CultureInfo("en-us")));

            Assert.AreEqual(block, map[new CultureInfo("zh-cn")].Keys.First());
        }
    }
}
