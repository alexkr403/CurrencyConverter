using Common.Language;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.CurrencyConverter.Engine;

namespace Server.CurrencyConverter.Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestConverter()
        {
            var usdConverter = new UsdConverter();

            var result0 = usdConverter.GetNumberPresentation(0);
            var result1 = usdConverter.GetNumberPresentation(1);
            var result25p1 = usdConverter.GetNumberPresentation(25.1m);
            var result0p01 = usdConverter.GetNumberPresentation(0.01m);
            var result45100 = usdConverter.GetNumberPresentation(45100);
            var result999999999p99 = usdConverter.GetNumberPresentation(999999999.99m);

            Assert.AreEqual(result0, "zero dollars");
            Assert.AreEqual(result1, "one dollar");
            Assert.AreEqual(result25p1, "twenty-five dollars and ten cents");
            Assert.AreEqual(result0p01, "zero dollars and one cent");
            Assert.AreEqual(result45100, "forty-five thousand one hundred dollars");
            Assert.AreEqual(result999999999p99, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents");
        }

        [TestMethod]
        public void TestInputNumber()
        {
            var currencyConverterService = new CurrencyConverterService();

            var oddSymbol1 = currencyConverterService.GetNumberPresentation("11t");
            var oddSymbol2 = currencyConverterService.GetNumberPresentation("11.0");
            var bigNumber = currencyConverterService.GetNumberPresentation("999 999 999 999");

            Assert.AreEqual(Language.NotNumber, oddSymbol1.ErrorMessage);
            Assert.AreEqual(Language.NotNumber, oddSymbol2.ErrorMessage);
            Assert.AreEqual(Language.RangeOfNumber, bigNumber.ErrorMessage);
        }
    }
}
