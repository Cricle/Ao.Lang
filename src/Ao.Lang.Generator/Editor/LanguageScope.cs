using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Ao.Lang.Generator.Editor
{
    public abstract class LanguageScope<TLangBlock> : ILanguageScope<TLangBlock>
        where TLangBlock : ILangBlock
    {
        protected LanguageScope(FileInfo physicalFile)
        {
            PhysicalFile = physicalFile ?? throw new ArgumentNullException(nameof(physicalFile));
            langBlocks = new Lazy<IList<TLangBlock>>(Load);
        }
        private Lazy<IList<TLangBlock>> langBlocks;
        public FileInfo PhysicalFile { get; }

        public IList<TLangBlock> LangBlocks
        {
            get => langBlocks.Value;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                langBlocks = new Lazy<IList<TLangBlock>>(() => value);
                _ = langBlocks.Value;
            }
        }

        private IList<TLangBlock> Load()
        {
            if (PhysicalFile.Exists)
            {
                using (var fs = PhysicalFile.OpenRead())
                {
                    return LoadFromStream(fs);
                }
            }
            return new List<TLangBlock>(0);
        }
        public virtual bool Save()
        {
            if (langBlocks.IsValueCreated)
            {
                using (var fs = PhysicalFile.Open(FileMode.Create))
                {
                    return SaveToStream(fs, LangBlocks);
                }
            }
            return false;
        }
        protected abstract bool SaveToStream(Stream stream, IList<TLangBlock> langIdentities);
        protected abstract IList<TLangBlock> LoadFromStream(Stream stream);

        public virtual void Compile(ILangIdentityCompiler compiler)
        {
            var map = LangBlocks.ToCultureMap<TLangBlock, ILangIdentity>();
            foreach (var item in map)
            {
                var kvstr = item.Value.ToDictionary(x => compiler.Compile(x.Key), x => x.Value);
                CompileItem(compiler, item.Key, kvstr);
            }
        }
        protected abstract void CompileItem(ILangIdentityCompiler compiler,
            CultureInfo culture,
            Dictionary<string, string> datas);
    }
}
