using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace System.Windows.Controls
{
    public static class LangExtensions
    {
        public static void BindText(this TextBlock block, string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            BindLang(block, TextBlock.TextProperty,
                key,
                args, defaultValue,
                fixedCulture,
                noUpdate);
        }
        public static void BindText(this ContentControl label, string key,
           object[] args = null,
           string defaultValue = null,
           string fixedCulture = null,
           bool noUpdate = false)
        {
            BindLang(label, ContentControl.ContentProperty,
                key,
                args, defaultValue,
                fixedCulture,
                noUpdate);
        }
        public static void BindText(this Run label, string key,
          object[] args = null,
          string defaultValue = null,
          string fixedCulture = null,
          bool noUpdate = false)
        {
            BindLang(label, Run.TextProperty,
                key,
                args, defaultValue,
                fixedCulture,
                noUpdate);
        }
        public static Binding CreateLangBinding(string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {

            var box = new LangStrBox
            {
                Args = args,
                Key = key,
                DefaultValue = defaultValue,
                FixedCulture = fixedCulture
            };
            if (noUpdate)
            {
                box.UpdateValue();
            }
            else
            {
                box.Init();
            }
            var binding = new Binding(nameof(LangStrBox.Value))
            {
                Source = box,
                Mode = BindingMode.OneWay
            };
            return binding;
        }
        public static void BindLang(this FrameworkElement fe,
            DependencyProperty property,
            string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            var binding = CreateLangBinding(key, args, defaultValue, fixedCulture, noUpdate);
            fe.SetBinding(property, binding);
        }
        public static void BindLang(this FrameworkContentElement fe,
            DependencyProperty property,
            string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            var binding = CreateLangBinding(key, args, defaultValue, fixedCulture, noUpdate);
            fe.SetBinding(property, binding);
        }
    }
}
