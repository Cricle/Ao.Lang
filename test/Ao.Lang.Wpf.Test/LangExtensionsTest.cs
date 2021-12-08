#if !NET5_0

using Ao.Lang.Runtime;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Ao.Lang.Wpf.Test
{
    [TestClass]
    public class LangExtensionsTest
    {
        [TestMethod]
        [STAThread]
        public void GivenNullCall_MustThrowException()
        {
            var tbx = new TextBlock();
            var run = new Run();
            var dp = TextBlock.TextProperty;
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindLang((FrameworkElement)null, dp, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindLang((FrameworkContentElement)null, dp, "a"));

            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindLang(tbx, dp, null));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindLang(run, dp, null));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindLang(tbx, null, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindLang(run, null, "a"));

            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindLang(tbx, null, dp, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindLang(run, null, dp, null));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindLang(tbx, null, dp, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindLang(run, null, dp, "a"));

            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindText((TextBlock)null, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindText((Run)null, "a"));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindText(tbx, null));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.BindText(run, null));

            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.CreateLangBinding(null,"a"));
            Assert.ThrowsException<ArgumentNullException>(() => LangExtensions.CreateLangBinding(LanguageManager.Instance,null));
        }
        [TestMethod]
        [STAThread]
        public void CreateBind_MustReturnBinding()
        {
            var bind = LangExtensions.CreateLangBinding(LanguageManager.Instance,"hello");
            Assert.IsNotNull(bind);
        }
        [TestMethod]
        [STAThread]
        public void BindText_MustBeBinded()
        {
            var tbx = new TextBlock();
            var run = new Run();
            LangExtensions.BindText(tbx, "hello");
            var tbxBind = tbx.GetBindingExpression(TextBlock.TextProperty);
            Assert.IsNotNull(tbxBind);
            LangExtensions.BindText(run, "hello");
            var runBind = run.GetBindingExpression(Run.TextProperty);
            Assert.IsNotNull(runBind);

            LangExtensions.BindText(tbx,new LanguageManager(), "hello");
            tbxBind = tbx.GetBindingExpression(TextBlock.TextProperty);
            Assert.IsNotNull(tbxBind);
            LangExtensions.BindText(run, new LanguageManager(), "hello");
            runBind = run.GetBindingExpression(Run.TextProperty);
            Assert.IsNotNull(runBind);

        }
        [TestMethod]
        [STAThread]
        public void BindProperty_MustBeBinded()
        {
            var tbx = new TextBlock();
            var run = new Run();
            LangExtensions.BindLang(tbx, TextBlock.TextProperty, "hello");
            var tbxBind = tbx.GetBindingExpression(TextBlock.TextProperty);
            Assert.IsNotNull(tbxBind);
            LangExtensions.BindLang(run, Run.TextProperty, "hello");
            var runBind = run.GetBindingExpression(Run.TextProperty);
            Assert.IsNotNull(runBind);

            LangExtensions.BindLang(tbx, new LanguageManager(), TextBlock.TextProperty, "hello");
            tbxBind = tbx.GetBindingExpression(TextBlock.TextProperty);
            Assert.IsNotNull(tbxBind);
            LangExtensions.BindLang(run, new LanguageManager(), Run.TextProperty, "hello");
            runBind = run.GetBindingExpression(Run.TextProperty);
            Assert.IsNotNull(runBind);
        }
    }
}

#endif