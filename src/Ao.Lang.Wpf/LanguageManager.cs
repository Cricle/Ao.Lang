
using Ao.Lang;
using System.Globalization;

namespace System.Windows.Data
{
    public class LanguageManager
    {
        public static readonly LanguageManager Instance = new LanguageManager();
        private CultureInfo cultureInfo = CultureInfo.CurrentUICulture;
        private ILanguageService langService= LanguageService.Default;
        public ILanguageService LangService 
        {
            get => langService;
            set 
            {
                if (value ==null)
                {
                    throw new ArgumentNullException(nameof(LangService));
                }
                langService = LangService;
            }
        }
        public CultureInfo CultureInfo
        {
            get => cultureInfo;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(CultureInfo));
                }
                cultureInfo = value;
                CultureInfoChanged?.Invoke(value);
            }
        }
        public ILanguageRoot Root => LangService.GetRoot(CultureInfo);

        public event Action<CultureInfo> CultureInfoChanged;

    }
}
