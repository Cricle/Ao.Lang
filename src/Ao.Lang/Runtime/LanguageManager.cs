using System;
using System.Globalization;

namespace Ao.Lang.Runtime
{
    public class LanguageManager
    {
        public static readonly LanguageManager Instance = new LanguageManager();

        private CultureInfo cultureInfo = CultureInfo.CurrentUICulture;
        private CultureInfo defaultCultureInfo = CultureInfo.CurrentUICulture;

        private ILanguageService langService = LanguageService.Default;

        public ILanguageService LangService
        {
            get => langService;
            set
            {
                if (langService != value)
                {
                    langService = value ?? throw new ArgumentNullException(nameof(value));
                    LangServiceChanged?.Invoke(value);
                }
            }
        }

        public CultureInfo CultureInfo
        {
            get => cultureInfo;
            set
            {
                if (cultureInfo != value)
                {
                    cultureInfo = value ?? throw new ArgumentNullException(nameof(value));
                    CultureInfoChanged?.Invoke(value);
                }
            }
        }

        public CultureInfo DefaultCultureInfo
        {
            get => defaultCultureInfo;
            set
            {
                if (defaultCultureInfo != value)
                {
                    defaultCultureInfo = value ?? throw new ArgumentNullException(nameof(value));
                    CultureInfoChanged?.Invoke(value);
                }
            }
        }

        public ILanguageRoot Root => LangService.GetRoot(CultureInfo);
        public ILanguageRoot DefaultRoot => LangService.GetRoot(DefaultCultureInfo);

        public event Action<CultureInfo> CultureInfoChanged;

        public event Action<ILanguageService> LangServiceChanged;
    }
}