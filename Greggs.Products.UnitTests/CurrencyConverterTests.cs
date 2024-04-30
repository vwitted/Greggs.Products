using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Greggs.Products.Api.Services;
using Greggs.Products.Api.Models;


namespace Greggs.Products.UnitTests
{
    public class CurrencyConverterTests
    {
        private readonly CurrencyConverter currencyConverter;

        public CurrencyConverterTests()
        {
            currencyConverter = new CurrencyConverter();
        }

        [Fact]
        public void ConvertCurrency_ValidCurrencyCodes_ReturnsAmountInEUR()
        {
            // Arrange
            decimal amount = 100;
            string sourceCurrencyCode = "GBP";
            string targetCurrencyCode = "EUR";
            decimal expectedAmount = 110m;

            // Act
            decimal result = currencyConverter.ConvertCurrency(amount, sourceCurrencyCode, targetCurrencyCode);

            // Assert
            Assert.Equal(expectedAmount, result);
        }

        [Fact]
        public void ConvertCurrency_ValidCurrencies_ReturnsAmountInEUR()
        {
            // Arrange
            decimal amount = 100;
            Currencies sourceCurrency = Currencies.GBP;
            Currencies targetCurrency = Currencies.EUR;
            decimal expectedAmount = 110m;

            // Act
            decimal result = currencyConverter.ConvertCurrency(amount, sourceCurrency, targetCurrency);

            // Assert
            Assert.Equal(expectedAmount, result);
        }

        [Fact]
        public void ConvertCurrency_InvalidCurrencyCode_ThrowsException()
        {
            // Arrange
            decimal amount = 100;
            string sourceCurrency = "GBP";
            string targetCurrency = "USD"; // Assuming USD is not defined in the ExchangeRates dictionary

            // Act & Assert
            Assert.Throws<Exception>(() => currencyConverter.ConvertCurrency(amount, sourceCurrency, targetCurrency));
        }

        [Theory]
        [InlineData("GBP", Currencies.GBP)]
        [InlineData("EUR", Currencies.EUR)]
        public void GetCurrenciesFromString_ValidCurrency_ReturnsCurrenciesEnum(string currencyString, Currencies expectedCurrency)
        {
            // Act
            Currencies result = currencyConverter.GetCurrenciesFromString(currencyString);

            // Assert
            Assert.Equal(expectedCurrency, result);
        }
    }
 }
