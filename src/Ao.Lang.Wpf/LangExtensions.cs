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
            if (block is null)
            {
                throw new ArgumentNullException(nameof(block));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

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
            if (label is null)
            {
                throw new ArgumentNullException(nameof(label));
            }

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
            if (label is null)
            {
                throw new ArgumentNullException(nameof(label));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

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
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

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
            if (fe is null)
            {
                throw new ArgumentNullException(nameof(fe));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

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
            if (fe is null)
            {
                throw new ArgumentNullException(nameof(fe));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var binding = CreateLangBinding(key, args, defaultValue, fixedCulture, noUpdate);
            fe.SetBinding(property, binding);
        }
    }
}
