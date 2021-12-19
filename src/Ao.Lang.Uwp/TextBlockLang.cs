using Ao.Lang.Runtime;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#endif
{
    public class TextBlockLang : IControlLang
    {
        public static readonly TextBlockLang Instance = new TextBlockLang();

        private TextBlockLang() { }

        public Type SupportType { get; } = typeof(TextBlock);

        public void Bind(in ControlLangBindContext context)
        {
            var box = context.CreateLangStrBox();
            var tbx = (TextBlock)context.Object;
            tbx.SetBinding(TextBlock.TextProperty, new Binding
            {
                Source = box,
                Path = new PropertyPath(nameof(ILangStrBox.Value))
            });
        }
    }

    public class TextBoxLang : IControlLang
    {
        public static readonly TextBoxLang Instance = new TextBoxLang();

        private TextBoxLang() { }

        public Type SupportType { get; } = typeof(TextBox);

        public void Bind(in ControlLangBindContext context)
        {
            var box = context.CreateLangStrBox();
            var tbx = (TextBlock)context.Object;
            tbx.SetBinding(TextBlock.TextProperty, new Binding
            {
                Source = box,
                Path = new PropertyPath(nameof(ILangStrBox.Value))
            });
        }
    }
}
