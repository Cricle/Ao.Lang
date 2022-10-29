using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using CLang = Ao.Lang.Wpf.Lang;

namespace Ao.Lang.Wpf.Test
{
    [TestClass]
    public class LangTest
    {
        [TestMethod]
        public void InitUsingConstruct_MustInitProperty()
        {
            var l = new CLang();
            Assert.IsNull(l.Key);
            Assert.AreEqual(0, l.Args.Capacity);


            l = new CLang("hello");
            Assert.AreEqual("hello", l.Key);

        }
        class ValueProvideValueTarget : IProvideValueTarget
        {
            public object TargetObject { get; set; }

            public object TargetProperty { get; set; }
        }
        class ValueServiceProvider : IServiceProvider
        {
            public object Value { get; set; }

            public object GetService(Type serviceType)
            {
                return Value;
            }
        }
#if !NET5_0
        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void Bind_MustReturnBindingExpression(bool noUpdate)
        {
            var lang = new CLang("title");
            lang.NoUpdate = noUpdate;
            var tbx = new TextBlock();
            var target = new ValueProvideValueTarget
            {
                TargetObject = tbx,
                TargetProperty = TextBlock.TextProperty
            };
            var provider = new ValueServiceProvider { Value = target };
            var obj = lang.ProvideValue(provider);
            Assert.IsInstanceOfType(obj, typeof(BindingExpression));
        }
#endif
    }
}
