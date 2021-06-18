using System;

namespace Ao.Lang.Generator
{
    public class LangIdentityCompiler : ILangIdentityCompiler
    {
        public static readonly LangIdentityCompiler Default = new LangIdentityCompiler(":");

        public LangIdentityCompiler(string spliter)
        {
            Spliter = spliter ?? throw new ArgumentNullException(nameof(spliter));
        }

        public string Spliter { get; }

        public string Compile(ILangIdentity identity)
        {
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            return string.Join(Spliter, identity.GetIdentityBlocks());
        }
    }
}
