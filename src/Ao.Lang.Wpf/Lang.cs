
using Ao.Lang;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Markup;

namespace System.Windows.Data
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

        [ConstructorArgument("key")]
        public string Key { get; set; }

        public object[] Args { get; set; }

        public string FixedCulture { get; set; }

        public string DefaultValue { get; set; }

        public bool NoUpdate { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var box = new LangStrBox
            {
                Key = Key,
                Args = Args,
                FixedCulture = FixedCulture,
                DefaultValue = DefaultValue
            };
            if (NoUpdate)
            {
                box.UpdateValue();
            }
            else
            {
                box.Init();
            }
            var binding = new Binding(nameof(LangStrBox.Value)) { Source = box };
            if (target.TargetObject is Setter)
            {
                return binding;
            }
            else
            {
                return binding.ProvideValue(serviceProvider);
            }
        }
    }
}
