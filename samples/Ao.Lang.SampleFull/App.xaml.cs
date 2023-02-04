using Ao.Lang.Runtime;
using System.Threading;
using System.Windows;

namespace Ao.Lang.SampleFull
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            LangLoader.Load(Thread.CurrentThread.CurrentCulture.Name);
        }
    }

}
