using System.Collections.Generic;
using System.IO;

namespace Ao.Lang.Generator.Editor
{
    public interface ILanguageEditor<TLangBlock>
        where TLangBlock : ILangBlock
    {
        DirectoryInfo Workspace { get; }


        ILanguageScope<TLangBlock> GetScope(string identity);
        FileInfo GetCompiledFile(string identity, string culture);
        IEnumerable<FileInfo> EnumerateDesignFiles(SearchOption searchOption = SearchOption.TopDirectoryOnly);
        IEnumerable<FileInfo> EnumerateCompiledFiles(SearchOption searchOption = SearchOption.TopDirectoryOnly);
    }
}
