using System;
using System.Windows.Markup;

namespace Ao.Lang.SampleFull
{
    public class DesignViewModel : MarkupExtension
    {
        public DesignViewModel()
        {
            LangLoader.Load("zh-cn");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}
