using Common.Language;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Server.CurrencyConverter.Engine
{
    public class UsdConverter
    {
        private readonly Dictionary<int, string> _dictionary = new Dictionary<int, string>()
        {
            {1, Language.One},
            {2, Language.Two},
            {3, Language.Three},
            {4, Language.Four},
            {5, Language.Five},
            {6, Language.Six},
            {7, Language.Seven},
            {8, Language.Eight},
            {9, Language.Nine},
            {10, Language.Ten},
            {11, Language.Eleven},
            {12, Language.Twelve},
            {13, Language.Thirteen},
            {14, Language.Fourteen},
            {15, Language.Fifteen},
            {16, Language.Sixteen},
            {17, Language.Seventeen},
            {18, Language.Eighteen},
            {19, Language.Nineteen},
            {20, Language.Twenty},
            {30, Language.Thirty},
            {40, Language.Forty},
            {50, Language.Fifty},
            {60, Language.Sixty},
            {70, Language.Seventy},
            {80, Language.Eighty},
            {90, Language.Ninety},
        };

        private readonly Plurlization _plurlization;

        public UsdConverter()
        {
            _plurlization = Plurlization.Instance;
        }

        public string GetNumberPresentation(decimal number)
        {
            var truncatePart = (int)Math.Truncate(number);
            var decimalPart = (int)(number % 1 * 100);

            var truncatePartPresentation = ConvertTruncateNumberWithZero(truncatePart);
            var decimalPartPresentation = ConvertDecimalNumber(decimalPart);

            return 
                $"{truncatePartPresentation}{decimalPartPresentation}";
        }

        /// <summary>
        /// Get integer part of number
        /// </summary>
        private string ConvertTruncateNumberWithZero(int truncatePart)
        {
            var currencyName = _plurlization.PluralizeWord(
                Language.Dollar, 
                truncatePart
                );

            var truncatePartPresentation = Language.Zero;
            if (truncatePart > 0)
            {
                truncatePartPresentation = ConvertTruncateNumber(truncatePart);
            }

            return
                $"{truncatePartPresentation} {currencyName}";
        }

        private string ConvertTruncateNumber(int truncatePart)
        {
            var truncatePartPresentation = GetTruncatePartPresentation(truncatePart);

            return
                truncatePartPresentation;
        }

        /// <summary>
        /// Get decimal part of number
        /// </summary>
        private string ConvertDecimalNumber(int decimalPart)
        {
            if (decimalPart == 0)
            {
                return
                    string.Empty;
            }

            var decimalPartPresentation = GetTensOnesPartPresentation(decimalPart);

            var currencyNameDecimal = _plurlization.PluralizeWord(
                Language.Cent,
                decimalPart
                );

            return
                $" {Language.And} {decimalPartPresentation} {currencyNameDecimal}";
        }

        private int GetNumberDigits(int number)
        {
            return
                (int)(Math.Log10(number) + 1);
        }

        private string GetTruncatePartPresentation(int number)
        {
            var partPresentation = string.Empty;

            var numberDigits = GetNumberDigits(number);
            switch (numberDigits)
            {
                case 1: //ones'
                case 2: //tens'
                case 3: //hundreds'
                    {
                        partPresentation = GetHundredsPartPresentation(number);
                        break;
                    }

                case 4: //thousands'
                case 5:
                case 6:
                    {
                        var quantity = number / 1000;
                        var thousandsPartPresentation = GetHundredsPartPresentation(
                            quantity,
                            _plurlization.PluralizeWord(Language.Thousand, quantity)
                            );
                        var remainPartPresentation = ConvertTruncateNumber(number % 1000);

                        partPresentation = $"{thousandsPartPresentation} {remainPartPresentation}";
                        break;
                    }
                case 7: //millions'
                case 8:
                case 9:
                    {
                        var quantity = number / 1000000;
                        var millionsPartPresentation = GetHundredsPartPresentation(
                            quantity,
                            _plurlization.PluralizeWord(Language.Million, quantity)
                            );
                        var remainPartPresentation = ConvertTruncateNumber(number % 1000000);

                        partPresentation = $"{millionsPartPresentation} {remainPartPresentation}";
                        break;
                    }
            }

            //т.к. на русском языке компоновка предложения, в котором прописью пишится число, отличается от англ. построения предложения
            //например, на русском яз. сумма прописью пишется как "девятьсот девяносто девять миллионов девятьсот девяносто девять тысяч девятьсот девяносто девять долларов"
            //                                               а не "девять сотен девяносто-девять миллионов девять сотен девяносто-девять тысяч итд

            // нет необходимости адаптировать класс UsdConverter полностью под русский язык (по средствам if)
            // целесообразней выделить interface IUsdConverter, затем
            // реализовать отдельный класс для русскоязычного построения предложения
            // реализовать отдельный класс для англо-немецкого построения предложения

            // Но т.к. в задаче не сказано каким именно образом составлять предложение на русском, оставляю компановку для русского языка в англ. стиле

            // Для того чтобы адапировать склонения для цифр 1 и 2 воспользуюсь Replace.
            // Это не лучшее решение, но самое простое, для того чтобы адаптировать склонения на русском, без бессмысленного усложнения класса UsdConverter
            if (Thread.CurrentThread.CurrentCulture.Name.Contains("ru"))
            {
                partPresentation = partPresentation.Replace("один сотня", "одна сотня");
                partPresentation = partPresentation.Replace("два сотни", "две сотни");
                partPresentation = partPresentation.Replace("один тысяча", "одна тысяча");
                partPresentation = partPresentation.Replace("два тысячи", "две тысячи");
            }

            return
                partPresentation.Trim();
        }

        private string GetHundredsPartPresentation(
            int number,
            string nameOfClass = null
            )
        {
            var hundredsPartPresentation = string.Empty;

            var numberDigits = GetNumberDigits(number);

            int tensOnesPart;

            if (numberDigits == 3)
            {
                var quantity = number / 100;

                _dictionary.TryGetValue(quantity, out var hundredsWord);
                
                var hundred = _plurlization.PluralizeWord(Language.Hundred, quantity);

                hundredsPartPresentation = $"{hundredsWord} {hundred}";

                tensOnesPart = number % 100;
            }
            else
            {
                tensOnesPart = number;
            }

            var tensOnesWord = GetTensOnesPartPresentation(tensOnesPart);

            var hundredsTensOnesPartPresentation =
                string.IsNullOrEmpty(hundredsPartPresentation) && string.IsNullOrEmpty(tensOnesWord)
                    ||
                !string.IsNullOrEmpty(hundredsPartPresentation) && !string.IsNullOrEmpty(tensOnesWord)
                    ? $"{hundredsPartPresentation} {tensOnesWord}"
                    : $"{hundredsPartPresentation}{tensOnesWord}";

            return
                $"{hundredsTensOnesPartPresentation} {nameOfClass}";
        }

        private string GetTensOnesPartPresentation(int tensOnesPart)
        {
            var tensOnesResult = _dictionary.TryGetValue(tensOnesPart, out var tensOnesWord);

            if (!tensOnesResult)
            {
                var onesPart = tensOnesPart % 10;
                var tensPart = tensOnesPart - onesPart;

                var tensWordResult = _dictionary.TryGetValue(tensPart, out var tensWord);
                var onesWordResult = _dictionary.TryGetValue(onesPart, out var onesWord);

                if (tensWordResult && onesWordResult)
                {
                    tensOnesWord = $"{tensWord}-{onesWord}";
                }
                else if (tensWordResult)
                {
                    tensOnesWord = tensWord;
                }
                else if (onesWordResult)
                {
                    tensOnesWord = onesWord;
                }
                else
                {
                    tensOnesWord = string.Empty;
                }
            }

            return
                tensOnesWord;
        }
    }
}
