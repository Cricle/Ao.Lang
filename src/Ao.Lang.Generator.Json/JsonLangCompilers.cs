using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ao.Lang.Generator.Editor
{
    public static class JsonLangCompilers
    {
        public static string CompileJson<TLangIdentity>(this ILangIdentityCompiler compiler, IEnumerable<KeyValuePair<TLangIdentity, string>> identities)
            where TLangIdentity : ILangIdentity
        {
            if (compiler is null)
            {
                throw new System.ArgumentNullException(nameof(compiler));
            }

            if (identities is null)
            {
                throw new System.ArgumentNullException(nameof(identities));
            }

            return JsonHelper.Serialize(identities.ToDictionary(x => compiler.Compile(x.Key), x => x.Value));
        }

    }
}
