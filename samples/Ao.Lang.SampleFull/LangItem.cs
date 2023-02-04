using Ao.Lang.Runtime;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Ao.Lang.SampleFull
{
    public class LangItem
    {
        public LangItem(string culture, ILangStrBox box)
        {
            Culture = culture;
            Box = box;
            box.Start();
            ChangeCommand=new RelayCommand(()=>LanguageManager.Instance.SetCulture(Culture));
        }

        public string Culture { get; }

        public ILangStrBox Box { get; }

        public ICommand ChangeCommand { get; }
    }
}
