using Ao.Lang.Lookup;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Ao.Lang
{
    public static class LangLookupExtensions
    {
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
