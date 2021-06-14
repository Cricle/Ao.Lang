using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
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
    public class LanguageBuilderTest
    {
        class ValueConfigurationSource : IConfigurationSource
        {
            public IConfigurationProvider Build(IConfigurationBuilder builder)
            {
                return new NullConfigurationProvider();
            }
        }
        class NullChangeToken : IChangeToken
        {
            public bool HasChanged => false;

            public bool ActiveChangeCallbacks => false;

            public IDisposable RegisterChangeCallback(Action<object> callback, object state)
            {
                return null;
            }
        }
        class NullConfigurationProvider : IConfigurationProvider
        {
            public IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
            {
                yield break;
            }

            public IChangeToken GetReloadToken()
            {
                return new NullChangeToken();
            }

            public void Load()
            {
            }

            public void Set(string key, string value)
            {
            }

            public bool TryGet(string key, out string value)
            {
                value = null;
                return false;
            }
        }
        [TestMethod]
        public void GivenNullInit_MustThrowException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new LanguageBuilder(null));
        }
        [TestMethod]
        public void GivenAnyProviders_BuildIt_MustReturnRoot()
        {
            var culutre = new CultureInfo("zh-cn");
            var builder = new LanguageBuilder(culutre);
            Assert.AreEqual(culutre, builder.Culture);
            for (int i = 0; i < 10; i++)
            {
                builder.Add(new ValueConfigurationSource());
            }
            ILanguageRoot root = builder.Build();
            Assert.IsNotNull(root);
            Assert.AreEqual(culutre, root.Culture);

            IConfigurationRoot configRoot = builder.Build();
            Assert.IsNotNull(configRoot);
        }

    }
}
