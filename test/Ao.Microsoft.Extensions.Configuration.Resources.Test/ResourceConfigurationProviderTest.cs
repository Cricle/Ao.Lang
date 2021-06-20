using Microsoft.Extensions.Configuration.Resources;
using Microsoft.Extensions.FileProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace Ao.Microsoft.Extensions.Configuration.Resources.Test
{
    [TestClass]
    public class ResourceConfigurationProviderTest
    {
        [TestMethod]
        public void GivenResourceFile_Must()
        {
            var res = new ResourceWriter("a.resources");
            res.AddResource("a", "1");
            res.AddResource("b", "2");
            res.Close();
            res.Dispose();
            var provider = new ResourceConfigurationProvider(
                new ResourceConfigurationSource
                {
                    FileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory),
                    Path = "a.resources",
                    Optional = true,
                    ReloadOnChange = false
                });
            provider.Load();
            Assert.IsTrue(provider.TryGet("a", out var a));
            Assert.AreEqual("1", a);
            Assert.IsTrue(provider.TryGet("b", out var b));
            Assert.AreEqual("2", b);
        }
    }
}
