using System.Globalization;
using System.Threading;
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

        private void Button_Click_En(object sender, RoutedEventArgs e)
        {
            SetCultureInfo(new CultureInfo("en"));
        }

        private void Button_Click_De(object sender, RoutedEventArgs e)
        {
            SetCultureInfo(new CultureInfo("de-DE"));
        }

        private void Button_Click_Ru(object sender, RoutedEventArgs e)
        {
            SetCultureInfo(new CultureInfo("ru-RU"));
        }

        private void SetCultureInfo(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            PageFrame.Content = App.Root.Get<CurrencyConverterView>();
        }
    }
}
