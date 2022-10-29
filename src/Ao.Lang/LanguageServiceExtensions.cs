using Ao.Lang.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Ao.Lang
{
    public static class LanguageServiceExtensions
    {
        public static ILanguageNode EnsureGetLangNode(this ILanguageService service, string culture)
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
        public static void Add<TSource>(this ILanguageService service, string culutre, params TSource[] fileSources)
             where TSource : IConfigurationSource
        {
            var culture = new CultureInfo(culutre);
            Add(service, culture, fileSources);
        }
        public static List<FileInfo> AddFolder(this ILanguageService langSer, DirectoryInfo dir, IFileLanguageLoader fileLanguageLoader)
        {
            var fs = new List<FileInfo>();
            foreach (var item in dir.GetDirectories())
            {
                var cultureName = item.Name.Replace("_", "-");
                var culture = new CultureInfo(cultureName);
                foreach (var file in item.GetFiles())
                {
                    fileLanguageLoader.LoadCulture(langSer, culture, file);
                    fs.Add(file);
                }
            }
            return fs;
        }
    }
}
