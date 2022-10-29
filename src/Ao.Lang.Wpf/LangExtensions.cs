using Ao.Lang.Runtime;
#if WPF_PLATFORM
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
#elif AVALONIAUI_PLATFORM
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using System;
using DependencyObject = Avalonia.IAvaloniaObject;
using DependencyProperty = Avalonia.AvaloniaProperty;
#else
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
#endif

#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif WPF_PLATFORM
namespace System.Windows.Controls
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#elif AVALONIAUI_PLATFORM
namespace Ao.Lang.AvaloniaUI
#endif
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
#if !UWP_PLATFORM &&! AVALONIAUI_PLATFORM
        public static void BindText(this Run label, string key,
              object[] args = null,
              string defaultValue = null,
              string fixedCulture = null,
              bool noUpdate = false)
        {
            BindText(label, LanguageManager.Instance, key, args, fixedCulture, defaultValue, noUpdate);
        }
        public static void BindText(this Run label, LanguageManager langMgr, string key,
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
            BindLang(label, langMgr, Run.TextProperty,
                key,
                args, defaultValue,
                fixedCulture,
                noUpdate);
        }
#endif
        public static Binding CreateLangBinding(this LanguageManager langMgr,
            string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            var box = LangBindExtensions.CreateLangBox(langMgr, key, args, defaultValue, fixedCulture, noUpdate);
            var binding = new Binding
            {
#if AVALONIAUI_PLATFORM
                Path = nameof(ILangStrBox.Value),
#else
                Path = new PropertyPath(nameof(ILangStrBox.Value)),
#endif
                Source = box,
                Mode = BindingMode.OneWay
            };
            return binding;
        }
        public static void BindLang(this DependencyObject fe,
            DependencyProperty property,
            string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            BindLang(fe, LanguageManager.Instance, property, key, args, defaultValue, fixedCulture, noUpdate);
        }
        public static void BindLang(this DependencyObject fe, LanguageManager langMgr,
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

            var binding = CreateLangBinding(langMgr, key, args, defaultValue, fixedCulture, noUpdate);
#if AVALONIAUI_PLATFORM
            fe.Bind(property, binding);
#else
            BindingOperations.SetBinding(fe, property, binding);
#endif
        }
#if WPF_PLATFORM
        public static void BindLang(this FrameworkContentElement fe,
            DependencyProperty property,
            string key,
            object[] args = null,
            string defaultValue = null,
            string fixedCulture = null,
            bool noUpdate = false)
        {
            BindLang(fe, LanguageManager.Instance, property, key, args, defaultValue, fixedCulture, noUpdate);
        }
        public static void BindLang(this FrameworkContentElement fe, LanguageManager langMgr,
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

            var binding = CreateLangBinding(langMgr, key, args, defaultValue, fixedCulture, noUpdate);
            fe.SetBinding(property, binding);
        }
#endif
    }
}
