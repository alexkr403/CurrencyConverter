using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Client.Common;
using Client.CurrencyConverter.Plugin.Annotations;

namespace Client.CurrencyConverter.Plugin
{
    public class CurrencyConverterViewModel : INotifyPropertyChanged
    {
        private string _numberString;
        public string NumberString
        {
            get => _numberString;
            set
            {
                _numberString = value;
                OnPropertyChanged(nameof(NumberString));
            }
        }

        private string _numberPresentation;
        public string NumberPresentation
        {
            get => _numberPresentation;
            set
            {
                _numberPresentation = value;
                OnPropertyChanged(nameof(NumberPresentation));
            }
        }

        private string _error;
        public string Error
        {
            get => _error;
            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }
        
        public RelayCommand ExecuteConvert
        {
            get;
        }

        public CurrencyConverterViewModel(CurrencyConverterModel currencyConverterModel)
        {
            currencyConverterModel = currencyConverterModel ?? throw new ArgumentNullException(nameof(currencyConverterModel));

            ExecuteConvert = new RelayCommand(
                obj =>
                {
                    var numberPresentationResult = currencyConverterModel.GetNumberPresentation(NumberString);

                    if (!numberPresentationResult.Success)
                    {
                        Error = numberPresentationResult.ErrorMessage;
                        NumberPresentation = string.Empty;
                    }
                    else
                    {
                        Error = string.Empty;
                        NumberPresentation = numberPresentationResult.Number;
                    }
                },
                obj => !string.IsNullOrEmpty(NumberString)
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
