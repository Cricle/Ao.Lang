using System;
using System.Collections.Generic;
using System.Text;

namespace Ao.Lang.Generator.Editor
{
    public static class JsonLangManagerExtensions
    {
        public static JsonLanguageEditor<TLangBlock> CreateJsonLanguageEditor<TLangBlock>(this LangManager mgr)
               where TLangBlock : ILangBlock
        {
            if (mgr is null)
            {
                throw new ArgumentNullException(nameof(mgr));
            }

            return new JsonLanguageEditor<TLangBlock>(mgr.Workspace);
        }
    }
}
