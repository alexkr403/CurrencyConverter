using System;
using System.Collections.Generic;

namespace Server.CurrencyConverter.Engine
{
    public class UsdConverter
    {
        private const string Zero = "zero";
        private const string Thousand = "thousand";
        private const string Million = "million";
        private const string Hundred = "hundred";
        private const string Dollar = "dollar";
        private const string Dollars = "dollars";
        private const string Cent = "cent";
        private const string Cents = "cents";

        private readonly Dictionary<int, string> _dictionary = new Dictionary<int, string>()
        {
            {1, "one"},
            {2, "two"},
            {3, "three"},
            {4, "four"},
            {5, "five"},
            {6, "six"},
            {7, "seven"},
            {8, "eight"},
            {9, "nine"},
            {10, "ten"},
            {11, "eleven"},
            {12, "twelve"},
            {13, "thirteen"},
            {14, "fourteen"},
            {15, "fifteen"},
            {16, "sixteen"},
            {17, "seventeen"},
            {18, "eighteen"},
            {19, "nineteen"},
            {20, "twenty"},
            {30, "thirty"},
            {40, "forty"},
            {50, "fifty"},
            {60, "sixty"},
            {70, "seventy"},
            {80, "eighty"},
            {90, "ninety"},
        };

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
            var currencyName =
                truncatePart == 1
                    ? Dollar
                    : Dollars;

            var truncatePartPresentation = Zero;
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

            var currencyNameDecimal =
                decimalPart == 1
                    ? Cent
                    : Cents;

            return
                $" and {decimalPartPresentation} {currencyNameDecimal}";
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
                        var thousandsPartPresentation = GetHundredsPartPresentation(
                            number / 1000,
                            Thousand
                            );
                        var remainPartPresentation = ConvertTruncateNumber(number % 1000);

                        partPresentation = $"{thousandsPartPresentation} {remainPartPresentation}";
                        break;
                    }
                case 7: //millions'
                case 8:
                case 9:
                    {
                        var millionsPartPresentation = GetHundredsPartPresentation(
                            number / 1000000,
                            Million
                            );
                        var remainPartPresentation = ConvertTruncateNumber(number % 1000000);

                        partPresentation = $"{millionsPartPresentation} {remainPartPresentation}";
                        break;
                    }
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
                _dictionary.TryGetValue(number / 100, out var hundredsWord);

                hundredsPartPresentation = $"{hundredsWord} {Hundred}";

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
