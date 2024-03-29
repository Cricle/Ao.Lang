﻿using Ao.Lang.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows.Markup;

namespace Ao.Lang.Wpf.Preview
{
    public class DesignViewModel : MarkupExtension
    {
        public DesignViewModel()
        {
            LangLoader.Load("en-us");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
