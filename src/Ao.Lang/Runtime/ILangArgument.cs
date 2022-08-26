using System;

namespace Ao.Lang.Runtime
{
    public interface ILangArgument
    {
        object Value { get; }

        event EventHandler Updated;
    }
}
