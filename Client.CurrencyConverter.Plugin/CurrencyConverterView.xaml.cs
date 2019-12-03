using System;
using System.Windows.Controls;

namespace Client.CurrencyConverter.Plugin
{
    public partial class CurrencyConverterView : UserControl
    {
        /// <summary>
        /// For access from Toolbox
        /// </summary>
        public CurrencyConverterView()
        {
            InitializeComponent();
        }

        public CurrencyConverterView(CurrencyConverterViewModel currencyConverterViewModel)
        {
            InitializeComponent();

            DataContext = currencyConverterViewModel ?? throw new ArgumentNullException(nameof(currencyConverterViewModel));
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            EnteredNumber.Focus();
        }
    }
}
