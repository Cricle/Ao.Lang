using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Ao.Lang.Runtime
{
    public interface ILanguageLoader<TInput>
    {
        void LoadCulture(ILanguageService langSer,CultureInfo culture,TInput input);
    }
}
