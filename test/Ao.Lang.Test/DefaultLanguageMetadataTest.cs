using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;

namespace Ao.Lang.Test
{
    [TestClass]
    public class DefaultLanguageMetadataTest
    {
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DefaultLanguageMetadata(null));
            Assert.ThrowsException<ArgumentNullException>(() => new DefaultLanguageMetadata(null, Enumerable.Empty<IConfigurationSource>()));
            Assert.ThrowsException<ArgumentNullException>(() => new DefaultLanguageMetadata(new CultureInfo("zh-cn"), null));
        }
        [TestMethod]
        public void GivenCultureInit_PropertyValueMustEqualInput()
        {
            var c = new CultureInfo("zh-cn");
            var m = new DefaultLanguageMetadata(c);
            Assert.AreEqual(c, m.Culture);
        }
        [TestMethod]
        public void GivenCultureInitAndCapacity_PropertyValueMustEqualInput()
        {
            var c = new CultureInfo("zh-cn");
            var m = new DefaultLanguageMetadata(c, 9);
            Assert.AreEqual(c, m.Culture);
            Assert.AreEqual(9, m.Capacity);
        }
        [TestMethod]
        public void GivenAnySourcesInit_PropertyValueMustEqualInput()
        {
            var c = new CultureInfo("zh-cn");
            var source = new IConfigurationSource[]
            {
                new MemoryConfigurationSource()
            };
            var m = new DefaultLanguageMetadata(c, source);
            Assert.AreEqual(source[0], m.Single());
            Assert.AreEqual(source[0], ((IEnumerable)m).OfType<IConfigurationSource>().Single());
        }
    }
}
