using System.Windows;
using Client.CurrencyConverter.Plugin;
using Ninject;

namespace Client.Start
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = App.Root.Get<CurrencyConverterView>();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            var currencyConverterServiceProxy = App.Root.Get<CurrencyConverterServiceClient>();
            currencyConverterServiceProxy.Abort();
        }
    }
}
