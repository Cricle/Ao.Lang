using Ao.Lang.Lookup;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace Ao.Lang
{
    /// <summary>
    /// 对类型<see cref="ILanguageMetadata"/>的扩展
    /// </summary>
    public static class LanguageMetadataExtensions
    {
        public static string[] RaiseAssemblyResources<T>(this ILanguageService langSer,
            int langRevIndex)
        {
            return RaiseAssemblyResources(langSer, typeof(T).Assembly, langRevIndex);
        }

        public static string[] RaiseAssemblyResources(this ILanguageService langSer,
            Assembly assembly,
            int langRevIndex)
        {
            return LangLookupExtensions.RaiseAssemblyResources(langSer,
                assembly,
                ".resources",
                langRevIndex, AddResource);
        }

        private static void AddResource(ILanguageNode node, Stream stream, string lang)
        {
            node.AddResourceStream(stream);
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
                else
                {
                    n.AddJsonStream(b.Stream);
                }
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
                else
                {
                    n.AddIniStream(b.Stream);
                }
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
                   else
                   {
                       n.AddXmlStream(b.Stream);
                   }
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