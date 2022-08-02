using Ao.Lang.Lookup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Ao.Lang
{
    public delegate void RaiseNodeHandle(ILanguageNode node,Stream stream, string lang);
    public static class LangLookupExtensions
    {
#if !NETSTANDARD1_1
        public static string[] RaiseAssemblyResources<TAssemblyType>(this ILanguageService langSer,
            string extensions,
            int langRevIndex,
            RaiseNodeHandle nodeAction)
        {
            if (langSer is null)
            {
                throw new ArgumentNullException(nameof(langSer));
            }

            var ass = typeof(TAssemblyType).Assembly;
            return RaiseAssemblyResources(langSer, ass, extensions, langRevIndex, nodeAction);
        }
#endif
        public static string[] RaiseAssemblyResources(this ILanguageService langSer,
            Assembly assembly,
            string extensions,
            int langRevIndex,
            RaiseNodeHandle nodeAction)
        {
            if (langSer is null)
            {
                throw new ArgumentNullException(nameof(langSer));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (nodeAction is null)
            {
                throw new ArgumentNullException(nameof(nodeAction));
            }

            var added = new List<string>();
            var names = assembly.GetManifestResourceNames();
            foreach (var item in names)
            {
                if (!item.EndsWith(extensions))
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
                        nodeAction(node,stream, lang);
                        added.Add(item);
                    }
                }
            }
            return added.ToArray();
        }
        public static void EnableFileType(this ILangLookup lookup,
           string extensions,
           Action<ILanguageNode, LangLookupBox> addition)
        {
            if (lookup is null)
            {
                throw new ArgumentNullException(nameof(lookup));
            }

            if (string.IsNullOrEmpty(extensions))
            {
                throw new ArgumentException($"“{nameof(extensions)}”不能为 null 或空。", nameof(extensions));
            }

            if (addition is null)
            {
                throw new ArgumentNullException(nameof(addition));
            }
            lookup.Add((ser, box) =>
            {
                if (string.Equals(extensions, box.Extension, StringComparison.OrdinalIgnoreCase))
                {
                    var lang = box.GetLangIdentity('.');
                    if (!CultureInfoHelper.IsAvaliableCulture(lang))
                    {
                        return false;
                    }
                    var root = ser.EnsureGetLangNode(new CultureInfo(lang));
                    addition(root, box);
                    return true;
                }
                return false;
            });
        }

        public static ILangLookup MakeLookup(this ILanguageService langSer)
        {
            return new LangLookup(langSer);
        }
        public static void Add(this ILangLookup adders, AddLang addLang)
        {
            if (adders is null)
            {
                throw new ArgumentNullException(nameof(adders));
            }

            adders.Add(new DelegateLangLookupAdder(addLang));
        }
        public static bool Raise(this ILangLookup adders, string filePath)
        {
            if (adders is null)
            {
                throw new ArgumentNullException(nameof(adders));
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException($"“{nameof(filePath)}”不能为 null 或空。", nameof(filePath));
            }

            return Raise(adders, filePath, true, false);
        }
#if !NETSTANDARD1_1
        public static string[] RaiseDirectory(this ILangLookup adders,
            string folder)
        {
            if (adders is null)
            {
                throw new ArgumentNullException(nameof(adders));
            }

            if (string.IsNullOrEmpty(folder))
            {
                throw new ArgumentException($"“{nameof(folder)}”不能为 null 或空。", nameof(folder));
            }

            return RaiseDirectory(adders, folder, SearchOption.TopDirectoryOnly);
        }
        public static string[] RaiseDirectory(this ILangLookup adders,
            string folder,
            SearchOption searchOption)
        {
            if (adders is null)
            {
                throw new ArgumentNullException(nameof(adders));
            }

            if (string.IsNullOrEmpty(folder))
            {
                throw new ArgumentException($"“{nameof(folder)}”不能为 null 或空。", nameof(folder));
            }

            return RaiseDirectory(adders, folder, searchOption, true, false);
        }
        public static string[] RaiseDirectory(this ILangLookup adders,
            string folder,
            SearchOption searchOption,
            bool optional,
            bool reloadOnChanged)
        {
            if (adders is null)
            {
                throw new ArgumentNullException(nameof(adders));
            }

            if (string.IsNullOrEmpty(folder))
            {
                throw new ArgumentException($"“{nameof(folder)}”不能为 null 或空。", nameof(folder));
            }

            var files = Directory.GetFiles(folder, "*.*", searchOption);
            var okFiles = new List<string>();
            foreach (var item in files)
            {
                var ok = Raise(adders, item, optional, reloadOnChanged);
                if (ok)
                {
                    okFiles.Add(item);
                }
            }
            return okFiles.ToArray();
        }
#endif
        private static bool Raise(ILangLookup lookup, LangLookupBox box)
        {
            foreach (var item in lookup)
            {
                if (item.Add(lookup.LangService, box))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool Raise(this ILangLookup lookup,
            Stream stream)
        {
            if (lookup is null)
            {
                throw new ArgumentNullException(nameof(lookup));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var box = new LangLookupBox(null, stream, false, false);
            return Raise(lookup, box);
        }
        public static bool Raise(this ILangLookup lookup,
            string filePath,
            bool optional,
            bool reloadOnChanged)
        {
            if (lookup is null)
            {
                throw new ArgumentNullException(nameof(lookup));
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException($"“{nameof(filePath)}”不能为 null 或空。", nameof(filePath));
            }

            var box = new LangLookupBox(filePath, null, optional, reloadOnChanged);
            return Raise(lookup, box);
        }
    }
}
