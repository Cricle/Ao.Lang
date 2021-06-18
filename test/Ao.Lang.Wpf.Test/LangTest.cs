using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using CLang=System.Windows.Data.Lang;

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
            Assert.IsNull(l.Args);


            l = new CLang("hello");
            Assert.AreEqual("hello", l.Key);

            var args = new object[] { 1 };
            l = new CLang("h", args);
            Assert.AreEqual("h", l.Key);
            Assert.AreEqual(args, l.Args);

            l = new CLang("h", 1);
            Assert.AreEqual("h", l.Key);
            Assert.AreEqual(1, l.Args[0]);
            Assert.AreEqual(1, l.Args.Length);

            l = new CLang("h", 1, 2);
            Assert.AreEqual("h", l.Key);
            Assert.AreEqual(1, l.Args[0]);
            Assert.AreEqual(2, l.Args[1]);
            Assert.AreEqual(2, l.Args.Length);

            l = new CLang("h", 1, 2, 3);
            Assert.AreEqual("h", l.Key);
            Assert.AreEqual(1, l.Args[0]);
            Assert.AreEqual(2, l.Args[1]);
            Assert.AreEqual(3, l.Args[2]);
            Assert.AreEqual(3, l.Args.Length);


            l = new CLang("h", 1, 2, 3, 4);
            Assert.AreEqual("h", l.Key);
            Assert.AreEqual(1, l.Args[0]);
            Assert.AreEqual(2, l.Args[1]);
            Assert.AreEqual(3, l.Args[2]);
            Assert.AreEqual(4, l.Args[3]);
            Assert.AreEqual(4, l.Args.Length);
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
