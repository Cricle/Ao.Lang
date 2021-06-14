using System.Globalization;

namespace System.Windows.Data
{
    public static class LanguageManagerExtensions
    {
        public static void SetCulture(this LanguageManager mgr, string culture)
        {
            mgr.CultureInfo = new CultureInfo(culture);
        }
        public static bool IsCulture(this LanguageManager mgr, string culture)
        {
            return string.Equals(culture, mgr.CultureInfo.Name, StringComparison.OrdinalIgnoreCase);
        }
        public static bool SwitchIfNot(this LanguageManager mgr, string culture,CultureInfo newCulture)
        {
            if (IsCulture(mgr,culture))
            {
                mgr.CultureInfo = newCulture;
                return true;
            }
            return false;
        }
        public static bool SwitchIfNot(this LanguageManager mgr, string culture, string newCulture)
        {
            return SwitchIfNot(mgr, culture, new CultureInfo(newCulture));
        }
    }
}
