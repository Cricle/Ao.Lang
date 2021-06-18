using Ao.Lang.Generator.Editor;
using System;

namespace Ao.Lang.Generator
{
    public static class ResourcesLangManagerExtensions
    {
        
        public static ResxLanguageEditor<TLangBlock> CreateResxLanguageEditor<TLangBlock>(this LangManager mgr)
            where TLangBlock : ILangBlock
        {
            if (mgr is null)
            {
                throw new ArgumentNullException(nameof(mgr));
            }

            return new ResxLanguageEditor<TLangBlock>(mgr.Workspace);
        }
    }
}
