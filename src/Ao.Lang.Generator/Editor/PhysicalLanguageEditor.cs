using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Ao.Lang.Generator.Editor
{
    public abstract class PhysicalLanguageEditor<TLangBlock> : ILanguageEditor<TLangBlock>, ILangSourceProvider
        where TLangBlock : ILangBlock
    {
        protected PhysicalLanguageEditor(DirectoryInfo workspace)
        {
            Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
        }

        public abstract string DesignFileNameTemplate { get; }
        public abstract string CompiledFileNameTemplate { get; }
        public abstract string DesignFileExtensions { get; }
        public abstract string CompiledFileExtensions { get; }

        public DirectoryInfo Workspace { get; }

        public ILanguageScope<TLangBlock> GetScope(string identity)
        {
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            var fn = string.Format(DesignFileNameTemplate, identity);
            var path = Path.Combine(Workspace.FullName, fn);
            return CreateScope(new FileInfo(path), identity);
        }
        protected abstract ILanguageScope<TLangBlock> CreateScope(FileInfo file, string identity);
        public FileInfo GetCompiledFile(string identity, string culture)
        {
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            if (string.IsNullOrEmpty(culture))
            {
                throw new ArgumentException($"“{nameof(culture)}”不能为 null 或空。", nameof(culture));
            }

            ThrowHelper.ThrowIfCultureNotFound(culture);
            var fn = string.Format(CompiledFileNameTemplate, identity, culture);
            var path = Path.Combine(Workspace.FullName, fn);
            return new FileInfo(path);
        }
        public IEnumerable<FileInfo> EnumerateDesignFiles(SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Workspace.EnumerateFiles("*." + DesignFileExtensions, searchOption);
        }
        public IEnumerable<FileInfo> EnumerateCompiledFiles(SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return Workspace.EnumerateFiles("*." + CompiledFileExtensions, searchOption);
        }

        public void Add(IConfigurationBuilder configurationBuilder, string filePath, bool optional, bool reloadOnChanged)
        {
            if (configurationBuilder is null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException($"“{nameof(filePath)}”不能为 null 或空。", nameof(filePath));
            }

            var ext = Path.GetExtension(filePath);
            if (ext != "." + CompiledFileExtensions)
            {
                throw new InvalidOperationException($"Can not add file {filePath}, this instance only support {CompiledFileExtensions} files!");
            }
            AddCore(configurationBuilder, filePath, optional, reloadOnChanged);
        }
        protected abstract void AddCore(IConfigurationBuilder configurationBuilder, string filePath, bool optional, bool reloadOnChanged);
    }
}
