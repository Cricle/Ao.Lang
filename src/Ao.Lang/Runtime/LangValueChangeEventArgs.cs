using System;

namespace Ao.Lang.Runtime
{
    public class LangValueChangeEventArgs : EventArgs
    {
        public LangValueChangeEventArgs(string old, string @new)
        {
            Old = old;
            New = @new;
        }

        public string Old { get; }

        public string New { get; }
    }
}
