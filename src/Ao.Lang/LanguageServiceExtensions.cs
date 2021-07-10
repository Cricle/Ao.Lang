using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Ao.Lang
{
    public static class LanguageServiceExtensions
    {
        public static ILanguageNode EnsureGetLangNode(this ILanguageService service,string culture)
        {
            return service.EnsureGetLangNode(new CultureInfo(culture));
        }
        public static ILanguageRoot GetRoot(this ILanguageService service, string cultureName)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (string.IsNullOrEmpty(cultureName))
            {
                throw new ArgumentException("message", nameof(cultureName));
            }
            return service.GetRoot(new CultureInfo(cultureName));
        }
        public static ILanguageRoot GetCurrentRoot(this ILanguageService service)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            var cul = CultureInfo.CurrentCulture;
            return service.GetRoot(cul);
        }
        public static string GetCurrentValue(this ILanguageService service, string key)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var root = service.GetCurrentRoot();
            if (root != null)
            {
                return root[key];
            }
            return null;
        }
        
        public static bool CultureIsSupport(this ILanguageService service, string cultureName)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (string.IsNullOrEmpty(cultureName))
            {
                throw new ArgumentException($"“{nameof(cultureName)}”不能为 null 或空。", nameof(cultureName));
            }

            var cul = new CultureInfo(cultureName);
            return service.CultureIsSupport(cul);
        }
        public static void Add<TSource>(this ILanguageService service, 
            CultureInfo cultureInfo, params TSource[] fileSources)
            where TSource : IConfigurationSource
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            if (fileSources is null)
            {
                throw new ArgumentNullException(nameof(fileSources));
            }

            service.Add(new DefaultLanguageMetadata(cultureInfo, fileSources.OfType<IConfigurationSource>().ToArray()));
        }
        public static void AddFromCurrentCulture<TSource>(this ILanguageService service, params TSource[] fileSources)
              where TSource : IConfigurationSource
        {
            Add(service, CultureInfo.CurrentCulture, fileSources);
        }
        public static void Add<TSource>(this ILanguageService service,string culutre, params TSource[] fileSources)
             where TSource : IConfigurationSource
        {
            var culture = new CultureInfo(culutre);
            Add(service, culture, fileSources);
        }
    }
}
