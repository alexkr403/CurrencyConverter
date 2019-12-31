using System;
using System.Globalization;
using System.Threading;
using Common.Language;
using Server.CurrencyConverter.Engine;

namespace Server.CurrencyConverter
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly UsdConverter _usdConverter;

        public CurrencyConverterService()
        {
            _usdConverter = new UsdConverter();
        }

        public NumberPresentationResult GetNumberPresentation(string value)
        {
            try
            {
                var currentCulture = Thread.CurrentThread.CurrentCulture;

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                var resTryParse = decimal.TryParse(value.Replace(',','.'), out decimal number);
                Thread.CurrentThread.CurrentCulture = currentCulture;

                if (!resTryParse)
                {
                    return
                        new NumberPresentationResult(
                            false,
                            null,
                            Language.NotNumber
                            );
                }

                if (number < 0 || number > 999999999.99m)
                {
                    return
                        new NumberPresentationResult(
                            false,
                            null,
                            Language.RangeOfNumber
                            );
                }

                var numberPresentation = _usdConverter.GetNumberPresentation(number);

                return
                    new NumberPresentationResult(
                        true,
                        numberPresentation,
                        null
                        );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return
                    NumberPresentationResult.Error;
            }
        }
    }
}
