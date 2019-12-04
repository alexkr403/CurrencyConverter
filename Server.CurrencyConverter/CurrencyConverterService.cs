using System;
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
                //I think, that validation of input value should execute on the client side. But I didn't get additional information about it from Susann
                //So execute validation according to Task on the server side (as I understood)
                var resTryParse = decimal.TryParse(value, out decimal number);

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
