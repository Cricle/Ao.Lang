using Ao.Lang.Runtime;
using Avalonia;
using Avalonia.Controls;
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
            var btn = new Button { Content = "ÇÐ»»" };
            btn.Click += Btn_Click;

            if (Content is StackPanel sp)
            {
                sp.Children.Add(tbx);
                sp.Children.Add(btn);
            }
            else
                throw new System.Exception("Should be StackPanel");
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