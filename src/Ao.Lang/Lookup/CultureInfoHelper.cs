using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Ao.Lang.Lookup
{
    public static class CultureInfoHelper
    {
#if !NETSTANDARD1_1
        private static readonly HashSet<string> avaliableCultures = new HashSet<string>(
            CultureInfo.GetCultures(CultureTypes.AllCultures).Select(x => x.Name),
            StringComparer.OrdinalIgnoreCase);
#endif
        public static bool IsAvaliableCulture(string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"“{nameof(culture)}”不能为 null 或空。", nameof(culture));
            }
#if !NETSTANDARD1_1
            return avaliableCultures.Contains(culture);
#else
            try
            {
                new CultureInfo(culture);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
#endif
        }
    }
}
