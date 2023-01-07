using System;
using System.Globalization;

namespace Ao.Lang.Lookup
{
    public static class CultureInfoHelper
    {
        public static bool IsAvaliableCulture(string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"“{nameof(culture)}”不能为 null 或空。", nameof(culture));
            }
            try
            {
                // Todo: GetCultureInfoByIetfLanguageTag was Deprecated
                return (CultureInfo.GetCultureInfoByIetfLanguageTag(culture).CultureTypes & CultureTypes.UserCustomCulture) != CultureTypes.UserCustomCulture;
            }
            catch
            {
                return false;
            }
        }
    }
}