using System;
using System.ComponentModel;

namespace Ao.Lang.Runtime
{
    public class DefaultLangArgument : ILangArgument, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Updated;

        private object value;

        public object Value
        {
            get => value;
            set
            {
                this.value = value;
                PropertyChanged?.Invoke(this, LangStrBox.valueChangedEventArgs);
                Updated?.Invoke(this, EventArgs.Empty);
            }
        }

        public string GetValue(object obj)
        {
            return obj?.ToString();
        }
    }
}
