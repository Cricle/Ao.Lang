using Ao.Lang.Runtime;
using Windows.UI.Xaml;

#if UNO_PLATFORM
namespace Ao.Lang.Uno
#elif UWP_PLATFORM
namespace Ao.Lang.Uwp
#endif
{
    public readonly struct ControlLangBindContext
    {
        public readonly DependencyObject Object;

        public readonly DependencyPropertyChangedEventArgs Args;

        public readonly LanguageManager LangManager;

        public ControlLangBindContext(DependencyObject @object, 
            DependencyPropertyChangedEventArgs args, 
            LanguageManager langManager)
        {
            Object = @object;
            Args = args;
            LangManager = langManager;
        }

        public ILangStrBox CreateLangStrBox(string defaultValue = null)
        {
            return LangManager.CreateLangBox((string)Args.NewValue, defaultValue: defaultValue);
        }
    }
}
