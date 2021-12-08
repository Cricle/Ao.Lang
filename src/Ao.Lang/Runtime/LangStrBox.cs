using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Ao.Lang.Runtime
{
    internal class LangStrBox : ILangStrBox
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private static readonly PropertyChangedEventArgs valueChangedEventArgs = new PropertyChangedEventArgs(nameof(Value));

        private string value;

        public string Value
        {
            get => value;
            set
            {
                var old = this.value;
                if (old != value)
                {
                    this.value = value;
                    PropertyChanged?.Invoke(this, valueChangedEventArgs);
                    LangValueChangeEventArgs args = null;
                    if (ValueChanged != null)
                    {
                        args = new LangValueChangeEventArgs(old, value);
                        ValueChanged.Invoke(this, args);
                    }
                    if (MulLang!=null)
                    {
                        if (args == null)
                        {
                            args = new LangValueChangeEventArgs(old, value);
                        }
                        MulLang.ReceivedChanged(this, args);
                    }
                }
            }
        }

        public LanguageManager LangMgr { get; set; }

        public ILanguageRoot LangRoot { get; set; }

        public IMulLang MulLang { get; set; }

        public string Key { get; set; }

        public object[] Args { get; set; }

        public string FixedCulture { get; set; }

        public string DefaultValue { get; set; }

        IReadOnlyList<object> ILangStrBox.Args => Args;

        private IDisposable disposable;

        public event EventHandler<LangValueChangeEventArgs> ValueChanged;

        ~LangStrBox()
        {
            Dispose();
        }
        internal void Init()
        {
            if (string.IsNullOrEmpty(FixedCulture))
            {
                Regist();
                LangMgr.CultureInfoChanged += RaiseCultureInfoChanged;
                LangRoot = LangMgr.Root;
            }
            else
            {
                LangRoot = LangMgr.LangService.GetRoot(FixedCulture);
            }
            UpdateValue();
        }
        internal void SwitchRoot()
        {
            LangRoot = LangMgr.Root;

            Regist();
            UpdateValue();
        }

        internal void Regist()
        {
            if (LangRoot != null)
            {
                disposable = LangRoot.GetReloadToken()
                    .RegisterChangeCallback(x =>
                    {
                        disposable?.Dispose();
                        Regist();
                        UpdateValue();
                    }, null);
            }
        }
        internal void UpdateValue()
        {
            Value = LangRoot?[Key, Args] ?? DefaultValue;
        }

        private void RaiseCultureInfoChanged(CultureInfo cultureInfo)
        {
            SwitchRoot();
        }
        public void Dispose()
        {
            LanguageManager.Instance.CultureInfoChanged -= RaiseCultureInfoChanged;
            GC.SuppressFinalize(this);
        }
    }
}
