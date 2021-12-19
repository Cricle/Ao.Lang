using Ao.Lang.Runtime;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#endif
{
    public partial class LangBehavior : DependencyObject, IBehavior
    {
        public string Property
        {
            get { return (string)GetValue(PropertyProperty); }
            set { SetValue(PropertyProperty, value); }
        }


        public string Key
        {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }


        public LanguageManager LangManager
        {
            get { return (LanguageManager)GetValue(LangManagerProperty); }
            set { SetValue(LangManagerProperty, value); }
        }


        public Type TargetType
        {
            get { return (Type)GetValue(TargetTypeProperty); }
            set { SetValue(TargetTypeProperty, value); }
        }

        public static readonly DependencyProperty TargetTypeProperty =
            DependencyProperty.Register("TargetType", typeof(Type), typeof(LangBehavior), new PropertyMetadata(null));


        public static readonly DependencyProperty LangManagerProperty =
            DependencyProperty.Register("LangManager", typeof(LanguageManager), typeof(LangBehavior), new PropertyMetadata(null, OnChanged));


        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.Register("Key", typeof(string), typeof(LangBehavior), new PropertyMetadata(null, OnChanged));


        public static readonly DependencyProperty PropertyProperty =
            DependencyProperty.Register("Property", typeof(string), typeof(LangBehavior), new PropertyMetadata(null, OnChanged));

        private DependencyObject associatedObject;

        public DependencyObject AssociatedObject => associatedObject;

        private static void OnChanged(DependencyObject @object, DependencyPropertyChangedEventArgs e)
        {
            var target = (LangBehavior)@object;
            var type = target.TargetType ?? target.associatedObject?.GetType();
            if (type != null)
            {
                var dp = DependencyHelper.Get(type, target.Property + "Property");
                if (dp != null)
                {
                    target.Bind(target.associatedObject, dp, target.Key);
                }
            }
        }

        private void Bind(DependencyObject @object, DependencyProperty property, string key)
        {
            if (@object != null && property != null)
            {
                var langMgr = LangManager ?? LanguageManager.Instance;
                var box = langMgr.CreateLangBox(key);
                BindingOperations.SetBinding(@object, property, new Binding
                {
                    Source = box,
                    Path = new PropertyPath(nameof(ILangStrBox.Value))
                });
            }
        }

        public void Attach(DependencyObject associatedObject)
        {
            this.associatedObject = associatedObject;
            OnChanged(this, null);
        }

        public void Detach()
        {

        }
    }
}
