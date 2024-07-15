using Ao.Lang.Runtime;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace Ao.Lang.Wpf.Preview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ILangKey langKey = new LangKey();

        public MainWindow()
        {
            InitializeComponent();

            Btn.BindLang(ContentProperty, langKey,new object[] {"123"});

            Tbx.SetBinding(TextBox.TextProperty, new Binding { Source = langKey, Path = new PropertyPath("Key"), Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
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
