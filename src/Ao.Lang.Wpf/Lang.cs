using Ao.Lang.Runtime;
using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
#if WPF_PLATFORM
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;
#elif AVALONIAUI_PLATFORM
using Avalonia;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
#else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
#if AVALONIAUI_PLATFORM
    public class DependencyArgument : DefaultLangArgument
    {
        public DependencyArgument(AvaloniaObject @object, AvaloniaProperty property,Action<Binding> action=null)
        {
            Object = @object ?? throw new ArgumentNullException(nameof(@object));
            Property = property ?? throw new ArgumentNullException(nameof(property));
            var bd = new Binding(nameof(Value))
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            action?.Invoke(bd);
            @object.Bind(property, bd);
        }

        public AvaloniaObject Object { get; }

        public AvaloniaProperty Property { get; }
    }
#else
    public class DependencyArgument : DefaultLangArgument
    {
        public DependencyArgument(DependencyObject @object, DependencyProperty property, Action<Binding> action = null)
        {
            Object = @object ?? throw new ArgumentNullException(nameof(@object));
            Property = property ?? throw new ArgumentNullException(nameof(property));
            var bd = new Binding
            {
                Source = this,
                Path=new PropertyPath(nameof(Value)),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            action?.Invoke(bd);
            BindingOperations.SetBinding(@object, property, bd);
        }

        public DependencyObject Object { get; }

        public DependencyProperty Property { get; }
    }
#endif
    public class LangArgument :
#if WPF_PLATFORM || UWP_PLATFORM
        FrameworkElement
#elif UNO_PLATFORM
        AttachedDependencyObject
#elif AVALONIAUI_PLATFORM
        AvaloniaObject
#endif
        , ILangArgument
    {
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public event EventHandler Updated;

#if AVALONIAUI_PLATFORM
        public static readonly AvaloniaProperty ValueProperty =
            AvaloniaProperty.Register<LangArgument, object>("Value", notifying: (a, b) => ((LangArgument)a).UpdateChanged());
#else
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(LangArgument), new PropertyMetadata(null, OnValueChanged));


        private static void OnValueChanged(DependencyObject @object,DependencyPropertyChangedEventArgs args)
        {
            var o = (LangArgument)@object;
            o.UpdateChanged();
        }

#endif

#if UNO_PLATFORM
        public LangArgument()
            : base(null)
        {
        }
#endif
        private void UpdateChanged()
        {
            Updated?.Invoke(this, EventArgs.Empty);
        }
    }
#if !AVALONIAUI_PLATFORM
#if WPF_PLATFORM
    [ContentProperty(nameof(Args))]
#else
    [ContentProperty(Name = nameof(Args))]
#endif
#endif
    public class Lang : MarkupExtension
    {
        public Lang()
        {
        }

        public Lang(string key)
        {
            Key = key;
        }

        public LanguageManager LangMgr { get; set; }

        public string Key { get; set; }

        public List<object> Args { get; set; } = new List<object>(0);

        public string FixedCulture { get; set; }

        public string DefaultValue { get; set; }

        public bool NoUpdate { get; set; }

#if WPF_PLATFORM || AVALONIAUI_PLATFORM
        public override object ProvideValue(IServiceProvider serviceProvider)
#else
        protected override object ProvideValue()
#endif
        {
            if (string.IsNullOrEmpty(Key))
            {
                return null;
            }
#if WPF_PLATFORM
            var target = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
#endif
            var box = LangBindExtensions.CreateLangBox(LangMgr ?? LanguageManager.Instance,
                Key,
                Args,
                DefaultValue,
                FixedCulture,
                NoUpdate);
            box.Start();
#if WPF_PLATFORM
            if (Args!=null)
            {
                foreach (var item in Args)
                {
                    if (item is FrameworkElement ele)
                    {
                        ele.DataContext = target.TargetObject;
                    }
                }
            }
#endif
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
