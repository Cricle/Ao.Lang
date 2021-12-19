using Ao.Lang.Runtime;
using Avalonia.Data;
using System;

namespace Avalonia.Controls
{
    public static class LangExtensions
    {
        public static void BindText(this TextBlock block, string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            BindText(block, LanguageManager.Instance, key, args, defaultValue, fixedCulture, noUpdate);
        }
        public static void BindText(this TextBlock block, LanguageManager langMgr, string key,
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

            BindLang(block, langMgr, TextBlock.TextProperty,
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
            BindText(label, LanguageManager.Instance, key, args, defaultValue, fixedCulture, noUpdate);
        }
        public static void BindText(this ContentControl label, LanguageManager langMgr, string key,
           object[] args = null,
           string defaultValue = null,
           string fixedCulture = null,
           bool noUpdate = false)
        {
            if (label is null)
            {
                throw new ArgumentNullException(nameof(label));
            }

            BindLang(label, langMgr, ContentControl.ContentProperty,
                key,
                args, defaultValue,
                fixedCulture,
                noUpdate);
        }
        public static Binding CreateLangBinding(this LanguageManager langMgr,
            string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            var box = LangBindExtensions.CreateLangBox(langMgr, key, args, defaultValue, fixedCulture, noUpdate);
            var binding = new Binding(nameof(ILangStrBox.Value))
            {
                Source = box,
                Mode = BindingMode.OneWay
            };
            return binding;
        }
        public static void BindLang(this IAvaloniaObject fe,
            AvaloniaProperty property,
            string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            BindLang(fe, LanguageManager.Instance, property, key, args, defaultValue, fixedCulture, noUpdate);
        }
        public static void BindLang(this IAvaloniaObject fe, LanguageManager langMgr,
            AvaloniaProperty property,
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

            var binding = CreateLangBinding(langMgr, key, args, defaultValue, fixedCulture, noUpdate);
            fe.Bind(property, binding);
        }
        public static void BindLang(this AvaloniaObject fe,
            AvaloniaProperty property,
            string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            BindLang(fe, LanguageManager.Instance, property, key, args, defaultValue, fixedCulture, noUpdate);
        }    
    }
}
