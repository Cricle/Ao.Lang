using System.Collections.Generic;

namespace Ao.Lang.Generator
{
    public class DefaultLangIdentity : List<string>, ILangIdentity
    {
        public DefaultLangIdentity()
        {
        }

        public DefaultLangIdentity(IEnumerable<string> collection) 
            : base(collection)
        {
        }

        public DefaultLangIdentity(int capacity) : base(capacity)
        {
        }

        public string[] GetIdentityBlocks()
        {
            return ToArray();
        }
    }
}
