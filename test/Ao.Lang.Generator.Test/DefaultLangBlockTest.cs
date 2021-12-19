using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ao.Lang.Generator.Test
{
    [TestClass]
    public class DefaultLangBlockTest
    {
        [TestMethod]
        public void Init_CultureMapMustNotNull()
        {
            var block = new DefaultLangBlock();

            Assert.IsNotNull(block.CultureStringMapping);
            block = new DefaultLangBlock(new[] { "a" });

            Assert.IsNotNull(block.CultureStringMapping);
            block = new DefaultLangBlock(3);

            Assert.IsNotNull(block.CultureStringMapping);
        }
    }
}
