using Ao.Lang.Runtime;
using System;
using System.Diagnostics;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;

#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#endif
{
    public class RunLang : IControlLang
    {
        public static readonly RunLang Instance = new RunLang(typeof(Run));

        private RunLang(Type type) 
        {
            Debug.Assert(typeof(Run).IsAssignableFrom(type));
            SupportType = type;
        }

        public Type SupportType { get; }

        public void Bind(in ControlLangBindContext context)
        {
            var box = context.CreateLangStrBox();
            var tbx = (Run)context.Object;
            BindingOperations.SetBinding(tbx, TextBlock.TextProperty, new Binding
            {
                Source = box,
                Path = new PropertyPath(nameof(ILangStrBox.Value))
            });
        }
    }
}
