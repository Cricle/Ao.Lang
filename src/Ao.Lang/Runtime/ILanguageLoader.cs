using System.Globalization;

namespace Ao.Lang.Runtime
{
    public interface ILanguageLoader<TInput>
    {
        void LoadCulture(ILanguageService langSer, CultureInfo culture, TInput input);
    }
}
