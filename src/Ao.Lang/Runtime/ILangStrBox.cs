using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Ao.Lang.Runtime
{
    public interface ILangStrBox :INotifyPropertyChanged,IDisposable
    {
        string Value { get; }

        LanguageManager LangMgr { get; }

        ILanguageRoot LangRoot { get; }

        string Key { get; }

        IReadOnlyList<object> Args { get; }

        string FixedCulture { get; }

        string DefaultValue { get; }

        IMulLang MulLang { get; }

        event EventHandler<LangValueChangeEventArgs> ValueChanged;
    }
}
