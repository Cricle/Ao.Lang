using Ao.Lang.Runtime;
using Avalonia;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ao.Lang.AvaloniaUI
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

        [ConstructorArgument("key")]
        public string Key { get; set; }

        public object[] Args { get; set; }

        public string FixedCulture { get; set; }

        public string DefaultValue { get; set; }

        public bool NoUpdate { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var box = LangBindExtensions.CreateLangBox(LangMgr ?? LanguageManager.Instance,
                Key,
                Args,
                FixedCulture,
                DefaultValue,
                NoUpdate);
            var binding = new Binding(nameof(ILangStrBox.Value)) { Source = box };
            if (target.TargetObject is Setter)
            {
                return binding;
            }
            return binding.Initiate((IAvaloniaObject)target.TargetObject, (AvaloniaProperty)target.TargetProperty);
        }
    }
}
