using Ao.Lang.Runtime;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ao.Lang.Wpf.Sample
{
    public class Q
    {
        public string A { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class MulLang : INotifyPropertyChanged
        {
            private string myString;

            public string MyString
            {
                get => myString;
                set
                {
                    myString = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyString)));
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

        }
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            DataContext = new Q { A = "123" };
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                var textblock = new TextBlock { TextWrapping = TextWrapping.NoWrap };
                textblock.BindText("Ao.Lang.Wpf.Sample.Title" + i);
                Sp.Children.Add(textblock);
            }
            var m = new MulLang();
            LanguageManager.Instance.BindTo("Ao.Lang.Wpf.Sample.Title0", m, x => x.MyString).Dispose();
            ObjectBind.SetBinding(TextBlock.TextProperty, new Binding(nameof(MulLang.MyString))
            {
                Source = m
            });
            var box = LanguageManager.Instance.CreateLangBox("F1", new object[]
            {
                new DependencyArgument(Tbx,TextBox.TextProperty),
                new DefaultLangArgument{Value="123"}
            });

            box.Start();
            BindArgs.SetBinding(TextBlock.TextProperty, new Binding(nameof(ILangStrBox.Value))
            {
                Source = box
            });
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
