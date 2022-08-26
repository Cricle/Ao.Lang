using Ao.Lang.Runtime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;

namespace Ao.Lang.AvaloniaUI.Sample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            var tbx = new TextBlock();
            tbx.BindText("Title");
            var btn = new Button { Content="ÇÐ»»"};
            btn.Click += Btn_Click;
            var sp = Content as StackPanel;
            sp.Children.Add(tbx);
            sp.Children.Add(btn);
        }

        private void Btn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (LanguageManager.Instance.IsCulture("zh-cn"))
            {
                LanguageManager.Instance.SetCulture("en-us");
            }
            else
            {
                LanguageManager.Instance.SetCulture("zh-cn");
            }


        }
    }
}