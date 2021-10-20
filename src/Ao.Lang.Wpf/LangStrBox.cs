
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
        private static readonly PropertyChangedEventArgs valueEventArgs = new PropertyChangedEventArgs(nameof(Value));
        public event PropertyChangedEventHandler PropertyChanged;

        private string value;

        public string Value
        {
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    RaisePropertyChanged(valueEventArgs);
                }
            }
        }
        public ILanguageRoot LangRoot { get; set; }

        public string Key { get; set; }

        public object[] Args { get; set; }

        public Func<object[]> ArgFetcher { get; set; }

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
            var args = Args;
            if (ArgFetcher!=null)
            {
                args = ArgFetcher();
            }
            Value = LangRoot?[Key, args] ?? DefaultValue;
        }

        protected void RaisePropertyChanged<T>(ref T prop, T value, [CallerMemberName] string name = null)
        {
            if (!EqualityComparer<T>.Default.Equals(prop, value))
            {
                prop = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        protected void RaisePropertyChanged(PropertyChangedEventArgs eventArgs)
        {
            PropertyChanged?.Invoke(this, eventArgs);
        }
        protected void RaisePropertyChanged([CallerMemberName]string name=null)
        {
            RaisePropertyChanged(new PropertyChangedEventArgs(name));
        }
        private void RaiseCultureInfoChanged(CultureInfo cultureInfo)
        {
            SwitchRoot();
        }
        public void Dispose()
        {
            disposable?.Dispose();
            LanguageManager.Instance.CultureInfoChanged -= RaiseCultureInfoChanged;
            GC.SuppressFinalize(this);
        }
    }
}
