using Ao.Lang.Lookup;
using System.Globalization;

namespace Ao.Lang.Generator
{
    internal static class ThrowHelper
    {
        public static void ThrowIfCultureNotFound(string culture)
        {
            if (!CultureInfoHelper.IsAvaliableCulture(culture))
            {
                throw new CultureNotFoundException(culture);
            }
        }
    }
}
