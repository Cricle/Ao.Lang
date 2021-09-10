
using Ao.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Threading;

namespace Ao.Lang.Wpf
{
    internal class LangStrBox : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string value;

        public string Value
        {
            get => value;
            set
            {
                RaisePropertyChanged(ref this.value, value);
            }
        }
        public ILanguageRoot LangRoot { get; set; }

        public string Key { get; set; }

        public object[] Args { get; set; }

        public string FixedCulture { get; set; }

        public string DefaultValue { get; set; }

        private IDisposable disposable;

        ~LangStrBox()
        {
            Dispose();
        }
        internal void Init()
        {
            if (string.IsNullOrEmpty(FixedCulture))
            {
                Regist();
                LanguageManager.Instance.CultureInfoChanged += RaiseCultureInfoChanged;
                LangRoot = LanguageManager.Instance.Root;
            }
            else
            {
                LangRoot = LanguageManager.Instance.LangService.GetRoot(FixedCulture);
            }
            UpdateValue();
        }
        internal void SwitchRoot()
        {
            LangRoot = LanguageManager.Instance.Root;

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

        protected void RaisePropertyChanged<T>(ref T prop, T value, [CallerMemberName] string name = null)
        {
            if (!EqualityComparer<T>.Default.Equals(prop, value))
            {
                prop = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
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
