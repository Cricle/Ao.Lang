using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace Ao.Lang.Generator.Editor
{
    public static class ResourcesLangCompilers
    {
        public static Stream CompileResource<TLangIdentity>(this ILangIdentityCompiler compiler, IEnumerable<KeyValuePair<TLangIdentity, string>> identities)
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

            var mem = new MemoryStream();
            var res = new ResourceWriter(mem);
            foreach (var item in identities)
            {
                var k = compiler.Compile(item.Key);
                res.AddResource(k, item.Value);
            }
            res.Generate();
            return mem;
        }
    }
}
