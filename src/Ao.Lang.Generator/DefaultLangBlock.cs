using System.Collections.Generic;
using System;

namespace Ao.Lang.Generator
{
    public class DefaultLangBlock : DefaultLangIdentity, ILangBlock
    {
        public DefaultLangBlock()
        {
            CultureStringMapping = new CultureStringMapping();
        }

        public DefaultLangBlock(IEnumerable<string> collection) : base(collection)
        {
            CultureStringMapping = new CultureStringMapping();
        }

        public DefaultLangBlock(int capacity) : base(capacity)
        {
            CultureStringMapping = new CultureStringMapping();
        }

        public CultureStringMapping CultureStringMapping { get; }
    }
}
