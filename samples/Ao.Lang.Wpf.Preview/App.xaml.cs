using System.Windows;

namespace Ao.Lang.Wpf.Preview
{
    public partial class App : Application
    {
        public App()
        {
            LangLoader.Load();
        }
    }
}
