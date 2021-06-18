using System.Collections.Generic;
using System.IO;

namespace Ao.Lang.Generator.Editor
{
    public interface ILanguageScope<TLangBlock>
        where TLangBlock : ILangBlock
    {
        FileInfo PhysicalFile { get; }

        IList<TLangBlock> LangBlocks { get; set; }

        bool Save();

        void Compile(ILangIdentityCompiler compiler);
    }
}
