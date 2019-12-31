using System;
using System.Linq;
using Common.Language;

namespace Server.CurrencyConverter.Engine
{
    public static class Plurlization
    {
        public static string PluralizeWord(
            string dictionary,
            int quantity
            )
        {
            if (string.IsNullOrEmpty(dictionary) || !dictionary.Trim().Any())
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            var pluralForms = dictionary.Split(';');

            if (pluralForms.Length != 3)
            {
                throw new Exception(string.Format(Language.PlurlError, dictionary));
            }

            pluralForms = pluralForms.Select(t => t.Trim()).ToArray();

            return
                PluralizeWord(
                    quantity,
                    pluralForms[0], 
                    pluralForms[1], 
                    pluralForms[2]
                    );
        }

        /// <summary>
        /// Make plural form
        /// </summary>
        /// <param name="n">Count of entities</param>
        /// <param name="one">Word's form, when 1</param>
        /// <param name="two">Word's form, when 2..4</param>
        /// <param name="five">Word's form, when 5..9</param>
        private static string PluralizeWord(
            int n, 
            string one, 
            string two, 
            string five
            )
        {
            if (string.IsNullOrEmpty(one))
            {
                throw new ArgumentNullException(nameof(one));
            }

            if (string.IsNullOrEmpty(two))
            {
                throw new ArgumentNullException(nameof(two));
            }

            if (string.IsNullOrEmpty(five))
            {
                throw new ArgumentNullException(nameof(five));
            }

            if (n < 0)
            {
                n = Math.Abs(n);
            }

            var plural = n % 10 == 1 && n % 100 != 11 
                ? 0
                : n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20) 
                    ? 1 
                    : 2;

            string result;

            switch (plural)
            {
                case 1:
                    result = $"{two}";
                    break;
                case 2:
                    result = $"{five}";
                    break;
                default:
                    result = $"{one}";
                    break;
            }

            return result;
        }
    }
}
