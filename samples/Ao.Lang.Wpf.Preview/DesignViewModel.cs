using System;
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
