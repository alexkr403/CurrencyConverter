using System;
using Server.CurrencyConverter.Engine;

namespace Server.CurrencyConverter
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        public const string NotNumber = "It's not number";
        public const string OutOfRange = "Range of number from 0 up to 999999999,99";
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
                            NotNumber
                            );
                }

                if (number < 0 || number > 999999999.99m)
                {
                    return
                        new NumberPresentationResult(
                            false,
                            null,
                            OutOfRange
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
