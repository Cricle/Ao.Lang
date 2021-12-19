using System;
using System.Globalization;

namespace Ao.Lang.Runtime
{
    public static class LanguageManagerExtensions
    {
        public static void SetCulture(this LanguageManager mgr, string culture)
        {
            if (mgr is null)
            {
                throw new ArgumentNullException(nameof(mgr));
            }

            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"“{nameof(culture)}”不能为 null 或空。", nameof(culture));
            }

            mgr.CultureInfo = new CultureInfo(culture);
        }
        public static bool IsCulture(this LanguageManager mgr, string culture)
        {
            if (mgr is null)
            {
                throw new ArgumentNullException(nameof(mgr));
            }

            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"“{nameof(culture)}”不能为 null 或空。", nameof(culture));
            }

            return string.Equals(culture, mgr.CultureInfo.Name, StringComparison.OrdinalIgnoreCase);
        }
        public static bool SwitchIfNot(this LanguageManager mgr, string culture, CultureInfo newCulture)
        {
            if (mgr is null)
            {
                throw new ArgumentNullException(nameof(mgr));
            }

            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"“{nameof(culture)}”不能为 null 或空。", nameof(culture));
            }

            if (newCulture is null)
            {
                throw new ArgumentNullException(nameof(newCulture));
            }

            if (IsCulture(mgr, culture))
            {
                mgr.CultureInfo = newCulture;
                return true;
            }
            return false;
        }
        public static bool SwitchIfNot(this LanguageManager mgr, string culture, string newCulture)
        {
            if (mgr is null)
            {
                throw new ArgumentNullException(nameof(mgr));
            }

            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"“{nameof(culture)}”不能为 null 或空。", nameof(culture));
            }

            if (string.IsNullOrEmpty(newCulture))
            {
                throw new ArgumentException($"“{nameof(newCulture)}”不能为 null 或空。", nameof(newCulture));
            }

            return SwitchIfNot(mgr, culture, new CultureInfo(newCulture));
        }
    }
}
