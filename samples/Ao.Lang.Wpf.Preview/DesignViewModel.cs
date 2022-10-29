using Ao.Lang.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Ao.Lang.Wpf.Preview
{
    public class DesignViewModel : MarkupExtension
    {
        public DesignViewModel()
        {
            LangLoader.Load();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
