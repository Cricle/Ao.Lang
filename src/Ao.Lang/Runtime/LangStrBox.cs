using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Ao.Lang.Runtime
{
    internal class LangStrBox : ILangStrBox
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal static readonly PropertyChangedEventArgs valueChangedEventArgs = new PropertyChangedEventArgs(nameof(Value));

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
                    if (MulLang != null)
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

        private bool hasOutterArg;
        private IList args;

        public LanguageManager LangMgr { get; set; }

        public ILanguageRoot LangRoot { get; set; }

        public IMulLang MulLang { get; set; }

        public string Key { get; set; }

        public IList Args
        {
            get => args;
            set
            {
                args = value;
                hasOutterArg = value != null && value.OfType<ILangArgument>().Any();
            }
        }

        public string FixedCulture { get; set; }

        public string DefaultValue { get; set; }

        IReadOnlyList<object> ILangStrBox.Args => args?.Cast<object>().ToList();

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
            if (LangRoot != null)
            {
                if (hasOutterArg)
                {
                    var args = new object[Args.Count];
                    for (int i = 0; i < Args.Count; i++)
                    {
                        var arg = Args[i];
                        if (arg is ILangArgument getter)
                        {
                            args[i] = getter.Value;
                        }
                        else
                        {
                            args[i] = arg;
                        }
                    }
                    Value = LangRoot[Key, args] ?? DefaultValue;
                }
                else
                {
                    Value = LangRoot[Key, Args] ?? DefaultValue;
                }
            }
            else
            {
                Value = null;
            }

        }

        private void RaiseCultureInfoChanged(CultureInfo cultureInfo)
        {
            SwitchRoot();
        }
        public void Dispose()
        {
            Stop();
            LanguageManager.Instance.CultureInfoChanged -= RaiseCultureInfoChanged;
            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            if (args != null)
            {
                foreach (var arg in args)
                {
                    if (arg is ILangArgument npc)
                    {
                        npc.Updated += OnNpcUpdated;
                    }
                }
            }
        }

        private void OnNpcUpdated(object sender, EventArgs e)
        {
            UpdateValue();
        }

        public void Stop()
        {
            if (args != null)
            {
                foreach (var arg in args)
                {
                    if (arg is ILangArgument npc)
                    {
                        npc.Updated -= OnNpcUpdated;
                    }
                }
            }
        }
    }
}
