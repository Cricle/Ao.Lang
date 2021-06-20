using Ao.Lang.Lookup;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Ao.Lang
{
    /// <summary>
    /// 对类型<see cref="ILanguageMetadata"/>的扩展
    /// </summary>
    public static class LanguageMetadataExtensions
    {
        public static string[] RaiseAssemblyResources<TAssemblyType>(this ILanguageService langSer, int langRevIndex)
        {
            if (langSer is null)
            {
                throw new ArgumentNullException(nameof(langSer));
            }

            var ass = typeof(TAssemblyType).Assembly;
            return RaiseAssemblyResources(langSer, ass, langRevIndex);
        }
        public static string[] RaiseAssemblyResources(this ILanguageService langSer,
            Assembly assembly,
            int langRevIndex)
        {
            if (langSer is null)
            {
                throw new ArgumentNullException(nameof(langSer));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var added = new List<string>();
            var names = assembly.GetManifestResourceNames();
            foreach (var item in names)
            {
                if (!item.EndsWith(".resources"))
                {
                    continue;
                }
                var sps = item.Split('.');
                if (sps.Length > 1 && sps.Length >= langRevIndex)
                {
                    var lang = sps[sps.Length - langRevIndex - 1].Replace('_', '-');
                    if (CultureInfoHelper.IsAvaliableCulture(lang))
                    {
                        var stream = assembly.GetManifestResourceStream(item);
                        var node = langSer.EnsureGetLangNode(lang);
                        node.AddResourceStream(stream);
                        added.Add(item);
                    }
                }
            }
            return added.ToArray();
        }
        public static void EnableJson(this ILangLookup lookup)
        {
            if (lookup is null)
            {
                throw new ArgumentNullException(nameof(lookup));
            }

            lookup.EnableFileType("json", (n, b) =>
            {
                if (b.Stream == null)
                {
                    n.AddJsonFile(b.Path, b.Optional, b.ReloadOnChanged);
                }
#if !NET452
                else
                {
                    n.AddJsonStream(b.Stream);
                }
#endif
            });
        }
        public static void EnableIni(this ILangLookup lookup)
        {
            if (lookup is null)
            {
                throw new ArgumentNullException(nameof(lookup));
            }

            lookup.EnableFileType("ini", (n, b) =>
            {
                if (b.Stream == null)
                {
                    n.AddIniFile(b.Path, b.Optional, b.ReloadOnChanged);
                }
#if !NET452
                else
                {
                    n.AddIniStream(b.Stream);
                }
#endif

            });
        }
        public static void EnableXml(this ILangLookup lookup)
        {
            if (lookup is null)
            {
                throw new ArgumentNullException(nameof(lookup));
            }

            lookup.EnableFileType("xml", (n, b) =>
               {
                   if (b.Stream == null)
                   {
                       n.AddXmlFile(b.Path, b.Optional, b.ReloadOnChanged);
                   }
#if !NET452
                   else
                   {
                       n.AddXmlStream(b.Stream);
                   }
#endif
               });
        }
        public static void EnableYaml(this ILangLookup lookup)
        {
            if (lookup is null)
            {
                throw new ArgumentNullException(nameof(lookup));
            }

            lookup.EnableFileType("yaml", (n, b) =>
            {
                if (b.Stream == null)
                {
                    n.AddYamlFile(b.Path, b.Optional, b.ReloadOnChanged);
                }

            });
        }
        public static void EnableResources(this ILangLookup lookup)
        {
            lookup.EnableFileType("resources", (n, b) =>
            {
                if (b.Stream == null)
                {
                    n.AddResourceFile(b.Path, b.Optional, b.ReloadOnChanged);
                }
                else
                {
                    n.AddResourceStream(b.Stream);
                }
            });
        }
        public static void EnableResx(this ILangLookup lookup)
        {
            if (lookup is null)
            {
                throw new ArgumentNullException(nameof(lookup));
            }

            lookup.EnableFileType("resx", (n, b) =>
            {
                if (b.Stream == null)
                {
                    n.AddResxFile(b.Path, b.Optional, b.ReloadOnChanged);
                }
                else
                {
                    n.AddResxStream(b.Stream);
                }
            });
        }
        public static void EnableAll(this ILangLookup lookup)
        {
            if (lookup is null)
            {
                throw new ArgumentNullException(nameof(lookup));
            }

            EnableJson(lookup);
            EnableIni(lookup);
            EnableYaml(lookup);
            EnableXml(lookup);
            EnableResources(lookup);
            EnableResx(lookup);
        }
    }
}
