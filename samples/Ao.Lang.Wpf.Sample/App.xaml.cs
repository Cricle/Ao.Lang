using System;
using System.IO;
using System.Resources;
using System.Windows;
using System.Windows.Data;

namespace Ao.Lang.Wpf.Sample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var ser = LanguageManager.Instance.LangService;
            ser.RaiseAssemblyResources<App>(2);
        }
    }
}
