using Ao.Lang.Runtime;
using System;
#if WPF_PLATFORM
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;
#elif AVALONIAUI_PLATFORM
using Avalonia.Data;
using Avalonia.Markup.Xaml;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
#endif

#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif WPF_PLATFORM
namespace Ao.Lang.Wpf
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#elif AVALONIAUI_PLATFORM
namespace Ao.Lang.AvaloniaUI
#endif
{
    public class Lang : MarkupExtension
    {
        public Lang()
        {
        }

        public Lang(string key)
        {
            Key = key;
        }
        public Lang(string key, object arg0)
            : this(key)
        {
            Args = new object[] { arg0 };
        }
        public Lang(string key, object arg0, object arg1, object arg2, object arg3)
            : this(key)
        {
            Args = new object[] { arg0, arg1, arg2, arg3 };
        }
        public Lang(string key, object arg0, object arg1, object arg2)
            : this(key)
        {
            Args = new object[] { arg0, arg1, arg2 };
        }
        public Lang(string key, object arg0, object arg1)
            : this(key)
        {
            Args = new object[] { arg0, arg1 };
        }

        public Lang(string key, params object[] args)
            : this(key)
        {
            Args = args;
        }

        public LanguageManager LangMgr { get; set; }

        public string Key { get; set; }

        public object[] Args { get; set; }

        public string FixedCulture { get; set; }

        public string DefaultValue { get; set; }

        public bool NoUpdate { get; set; }
#if WPF_PLATFORM || AVALONIAUI_PLATFORM
        public override object ProvideValue(IServiceProvider serviceProvider)
#else
        protected override object ProvideValue()
#endif
        {
#if WPF_PLATFORM
            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
#endif
            var box = LangBindExtensions.CreateLangBox(LangMgr ?? LanguageManager.Instance,
                Key,
                Args,
                FixedCulture,
                DefaultValue,
                NoUpdate);
#if WPF_PLATFORM
            var binding = new Binding(nameof(ILangStrBox.Value)) { Source = box };
            if (target.TargetObject is Setter)
            {
                return binding;
            }
            return binding.ProvideValue(serviceProvider);
#elif AVALONIAUI_PLATFORM
            var binding = new Binding
            {
                Path = nameof(ILangStrBox.Value),
                Source = box
            };
            return binding;
#else
            var binding = new Binding
            {
                Path = new PropertyPath(nameof(ILangStrBox.Value)),
                Source = box
            };
            return binding;
#endif
        }
    }
}
