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
                ["Title"] = "БъЬт"
            });
            root = inst.LangService.EnsureGetLangNode("en-US");
            root.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["Title"] = "title"
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