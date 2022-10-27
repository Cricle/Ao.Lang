using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
