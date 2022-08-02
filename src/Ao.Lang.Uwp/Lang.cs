using Ao.Lang.Runtime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;

#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif WPF_PLATFORM
namespace Ao.Lang.Wpf
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#endif
{
    public class LangEx : MarkupExtension
    {
        public string Path { get; set; }

        public object Arg { get; set; }

        protected override object ProvideValue()
        {
            var box = LangBindExtensions.CreateLangBox(LanguageManager.Instance,
                Path,
                null,
                null,
                null,
                false);
            var bd = new Binding
            {
                Path= new PropertyPath(nameof(ILangStrBox.Value)),
                Source =box
            };
            return bd;
        }
    }
}
