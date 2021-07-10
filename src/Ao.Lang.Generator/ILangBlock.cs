using System.Collections.Generic;

namespace Ao.Lang.Generator
{
    public interface ILangBlock : ILangIdentity
    {
        CultureStringMapping CultureStringMapping { get; }
    }
}
