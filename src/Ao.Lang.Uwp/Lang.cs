using Ao.Lang.Runtime;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#endif
{
    public static class Lang
    {
        private static readonly Dictionary<Type, IControlLang> controlBindMap = new Dictionary<Type, IControlLang>();

        static Lang()
        {
            Add(TextBoxLang.Instance);
            Add(TextBlockLang.Instance);
            Add(RunLang.Instance);
            Add(ToolTipLang.Instance);
        }

        public static void Add(IControlLang lang)
        {
            if (lang is null)
            {
                throw new ArgumentNullException(nameof(lang));
            }
            controlBindMap[lang.SupportType] = lang;
        }

        private static LanguageManager langManager = LanguageManager.Instance;

        public static LanguageManager LangManager
        {
            get => langManager;
            set
            {
                langManager = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text", typeof(string), typeof(Lang), new PropertyMetadata(null, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject @object, DependencyPropertyChangedEventArgs e)
        {
            var key = (string)e.NewValue;
            if (!string.IsNullOrEmpty(key))
            {
                if (controlBindMap.TryGetValue(@object.GetType(), out var controlLang))
                {
                    var ctx = new ControlLangBindContext(@object, e, langManager);
                    controlLang.Bind(ctx);
                }
            }
        }
    }
}
