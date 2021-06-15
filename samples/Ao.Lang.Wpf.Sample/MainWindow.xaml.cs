using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace Ao.Lang.Wpf.Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                var textblock = new TextBlock();
                textblock.BindText("Title");
                Sp.Children.Add(textblock);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
