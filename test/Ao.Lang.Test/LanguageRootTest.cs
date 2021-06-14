using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Lang.Test
{
    [TestClass]
    public class LanguageRootTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new LanguageRoot(null,new IConfigurationProvider[0]));
        }
        [TestMethod]
        public void InitWithCultureInfo_PropertyValueMustEquals()
        {
            var culture = new CultureInfo("zh-cn");
            var root=new LanguageRoot(culture, new IConfigurationProvider[0]);
            Assert.AreEqual(culture, root.Culture);
        }
    }
}
