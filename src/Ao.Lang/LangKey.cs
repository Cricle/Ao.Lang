using System.ComponentModel;

namespace Ao.Lang
{
    public interface ILangKey : INotifyPropertyChanged
    {
        string Key { get; set; }
    }
    public class LangKey : ILangKey
    {
        private static readonly PropertyChangedEventArgs KeyChanged = new PropertyChangedEventArgs("Key");

        private string key;

        public string Key
        {
            get => key;
            set
            {
                key = value;
                PropertyChanged?.Invoke(this, KeyChanged);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{{Key:{key}}}";
        }
    }
}
