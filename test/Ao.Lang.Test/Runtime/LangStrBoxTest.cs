using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
