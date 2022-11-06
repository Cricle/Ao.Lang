using Ao.Lang.Runtime;
using System.Windows;

namespace Ao.Lang.Wpf.Preview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
