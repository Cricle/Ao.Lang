using Ao.Lang.Lookup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Lang.Test.Lookup
{
    [TestClass]
    public class CultureInfoHelperTest
    {
        [TestMethod]
        [DataRow("zh-cn")]
        [DataRow("zh-CN")]
        [DataRow("zh-tw")]
        [DataRow("en-us")]
        [DataRow("en-US")]
        public void GivenExistsCulutre_MustReturnTrue(string culture)
        {
            var exisits = CultureInfoHelper.IsAvaliableCulture(culture);
            Assert.IsTrue(exisits);
        }
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void GivenNullOrEmpty_MustThrowException(string culture)
        {
            Assert.ThrowsException<ArgumentException>(() => CultureInfoHelper.IsAvaliableCulture(culture));
        }
        [TestMethod]
        [DataRow("a")]
        [DataRow("b")]
        [DataRow("!@#")]
        [DataRow("---")]
        [DataRow("hello")]
        public void GivenNoExistsCulutre_MustReturnFalse(string culture)
        {
            var exisits = CultureInfoHelper.IsAvaliableCulture(culture);
            Assert.IsFalse(exisits);
        }
    }
}
