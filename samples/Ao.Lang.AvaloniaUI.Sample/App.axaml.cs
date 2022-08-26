using Ao.Lang.Runtime;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Ao.Lang.AvaloniaUI.Sample
{
    public class App : Application
    {
        public override void Initialize()
        {
            var inst = LanguageManager.Instance;
            var root = inst.LangService.EnsureGetLangNode("zh-CN");
            root.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["Title"] = "±êÌâ",
                ["F1"] = "ÄãºÃ{0}, ¹þ¹þ¹þ!"
            });
            root = inst.LangService.EnsureGetLangNode("en-US");
            root.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["Title"] = "title",
                ["F1"] = "Hello {0}, hahaha!"
            });

            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}