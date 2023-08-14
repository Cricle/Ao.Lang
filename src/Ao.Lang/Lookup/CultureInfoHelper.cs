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
            return avaliableCultures.Contains(culture);
        }
    }
}