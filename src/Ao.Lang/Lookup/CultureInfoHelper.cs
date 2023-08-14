using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ao.Lang.Lookup
{
    public static class CultureInfoHelper
    {
        private static readonly HashSet<string> avaliableCultures = new HashSet<string>(
            CultureInfo.GetCultures(CultureTypes.AllCultures).Select(x => x.Name),
            StringComparer.OrdinalIgnoreCase);
        public static bool IsAvaliableCulture(string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"“{nameof(culture)}”不能为 null 或空。", nameof(culture));
            }
            //NOTE: Actually, I don't want this detection to be implemented using try. If there is a better way to replace it
            if ("zh-cn".Equals(culture, StringComparison.OrdinalIgnoreCase)||
                "zh-tw".Equals(culture, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return avaliableCultures.Contains(culture);
        }
    }
}