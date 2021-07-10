﻿using System;
using System.IO;
using System.Resources;
using System.Windows;
using System.Windows.Data;
using Ao.Lang.Generator;
using Ao.Lang.Generator.Editor;
using Microsoft.Extensions.Configuration;

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
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Strings");
            var zhNode = ser.EnsureGetLangNode("zh-cn");
            zhNode.AddJsonFile(Path.Combine(path, "zh_cn", "hello.zh-cn.json"), false);
            var enNode = ser.EnsureGetLangNode("en-us");
            enNode.AddJsonFile(Path.Combine(path, "en_us", "hello.en-us.json"), false);
        }
    }
}
