using Ao.Lang.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.Lang.Wpf.Preview
{
    public class DesignViewModel
    {
        public DesignViewModel()
        {
            LangLoader.Load();
        }
    }
}
