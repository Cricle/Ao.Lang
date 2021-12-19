using Ao.Lang.Runtime;
using System;
using System.Diagnostics;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#endif
{
    public class ToolTipLang : IControlLang
    {
        public static readonly ToolTipLang Instance = new ToolTipLang(typeof(ToolTip));

        private ToolTipLang(Type type)
        {
            Debug.Assert(typeof(ToolTip).IsAssignableFrom(type));
            SupportType = type;
        }

        public Type SupportType { get; }

        public void Bind(in ControlLangBindContext context)
        {
            var box = context.CreateLangStrBox();
            var tt = (ToolTip)context.Object;
            BindingOperations.SetBinding(tt, ToolTip.ContentProperty, new Binding
            {
                Source = box,
                Path = new PropertyPath(nameof(ILangStrBox.Value))
            });
        }
    }
}
