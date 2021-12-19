using Ao.Lang.Generator.Editor;
using Ao.Lang.Lookup;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ao.Lang.Generator
{
    public static class PhysicalLanguageEditorExtensions
    {
        public static FileInfo[] FindAndAll<TLangBlock>(this PhysicalLanguageEditor<TLangBlock> editor,
            IConfigurationBuilder builder,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
                  where TLangBlock : ILangBlock
        {
            if (editor is null)
            {
                throw new System.ArgumentNullException(nameof(editor));
            }

            if (builder is null)
            {
                throw new System.ArgumentNullException(nameof(builder));
            }

            return FindAndAll(editor, builder, true, true, searchOption);
        }
        public static FileInfo[] FindAndAll<TLangBlock>(this PhysicalLanguageEditor<TLangBlock> editor,
            IConfigurationBuilder builder,
            bool optional, bool reloadOnChanged,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
                  where TLangBlock : ILangBlock
        {
            if (editor is null)
            {
                throw new System.ArgumentNullException(nameof(editor));
            }

            if (builder is null)
            {
                throw new System.ArgumentNullException(nameof(builder));
            }

            var files = editor.EnumerateCompiledFiles(searchOption);
            foreach (var item in files)
            {
                editor.Add(builder, item.FullName, optional, reloadOnChanged);
            }
            return files.ToArray();
        }
        public static ILanguageService ToLanguageService<TLangBlock>(this PhysicalLanguageEditor<TLangBlock> editor,
           SearchOption searchOption = SearchOption.TopDirectoryOnly)
                  where TLangBlock : ILangBlock
        {
            if (editor is null)
            {
                throw new System.ArgumentNullException(nameof(editor));
            }

            var langSer = new LanguageService();
            FindAndAll(editor, langSer, searchOption);
            return langSer;
        }
        public static ILanguageService ToLanguageService<TLangBlock>(this PhysicalLanguageEditor<TLangBlock> editor,
            bool optional, bool reloadOnChanged,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
                   where TLangBlock : ILangBlock
        {
            if (editor is null)
            {
                throw new System.ArgumentNullException(nameof(editor));
            }

            var langSer = new LanguageService();
            FindAndAll(editor, langSer, optional, reloadOnChanged, searchOption);
            return langSer;
        }
        public static FileInfo[] FindAndAll<TLangBlock>(this PhysicalLanguageEditor<TLangBlock> editor,
            ILanguageService langSer,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
                  where TLangBlock : ILangBlock
        {
            if (editor is null)
            {
                throw new System.ArgumentNullException(nameof(editor));
            }

            if (langSer is null)
            {
                throw new System.ArgumentNullException(nameof(langSer));
            }

            return FindAndAll(editor, langSer, true, true, searchOption);
        }
        public static FileInfo[] FindAndAll<TLangBlock>(this PhysicalLanguageEditor<TLangBlock> editor,
            ILanguageService langSer,
            bool optional, bool reloadOnChanged,
            SearchOption searchOption = SearchOption.TopDirectoryOnly)
            where TLangBlock : ILangBlock
        {
            if (editor is null)
            {
                throw new System.ArgumentNullException(nameof(editor));
            }

            if (langSer is null)
            {
                throw new System.ArgumentNullException(nameof(langSer));
            }

            var addedFiles = new List<FileInfo>();
            var files = editor.EnumerateCompiledFiles(searchOption);
            foreach (var item in files)
            {
                var sps = item.Name.Split('.');
                if (sps.Length > 1)
                {
                    var culture = sps[sps.Length - 2];
                    if (CultureInfoHelper.IsAvaliableCulture(culture))
                    {
                        var root = langSer.EnsureGetLangNode(culture);
                        editor.Add(root, item.FullName, optional, reloadOnChanged);
                        addedFiles.Add(item);
                    }
                }
            }
            return addedFiles.ToArray();
        }
    }
}
