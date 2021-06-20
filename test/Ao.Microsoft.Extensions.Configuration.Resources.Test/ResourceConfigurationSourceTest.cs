using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ao.Microsoft.Extensions.Configuration.Resources.Test
{
    [TestClass]
    public class ResourceConfigurationSourceTest
    {
        [TestMethod]
        public void Build_MustReturnProvider()
        {
            var s = new ResourceConfigurationSource();
            var provider=s.Build(new ConfigurationBuilder());
            Assert.IsNotNull(provider);
        }
    }
}
