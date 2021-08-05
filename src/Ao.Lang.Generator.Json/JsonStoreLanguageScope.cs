using System.Collections.Generic;
using System.IO;

namespace Ao.Lang.Generator.Editor
{
    public abstract class JsonStoreLanguageScope<TLangBlock> : LanguageScope<TLangBlock>
        where TLangBlock : ILangBlock
    {
        protected JsonStoreLanguageScope(FileInfo physicalFile)
            : base(physicalFile)
        {
        }

        protected override IList<TLangBlock> LoadFromStream(Stream stream)
        {
            var sr = new StreamReader(stream);
            var str = sr.ReadToEnd();
            return JsonHelper.Deserialize<List<TLangBlock>>(str);
        }

        protected override bool SaveToStream(Stream stream, IList<TLangBlock> langIdentities)
        {
            var str = JsonHelper.Serialize(langIdentities);
            var sw = new StreamWriter(stream);
            sw.Write(str);
            sw.Flush();
            return true;
        }
    }
}
