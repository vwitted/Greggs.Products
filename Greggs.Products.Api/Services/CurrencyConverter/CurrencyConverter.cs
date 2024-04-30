using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Services.CurrencyConverter
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private Dictionary<Tuple<Currencies, Currencies>, decimal> ExchangeRates = new Dictionary<Tuple<Currencies, Currencies>, decimal>()
        {
            { Tuple.Create(Currencies.GBP,Currencies.EUR), 1.1m }
        };

        public decimal ConvertCurrency(decimal amount, string sourceCurrencyCode, string targetCurrencyCode)
        {
            return ConvertCurrency(amount, GetCurrenciesFromString(sourceCurrencyCode), GetCurrenciesFromString(targetCurrencyCode));
        }

        public decimal ConvertCurrency(decimal amount, Currencies sourceCurrency, Currencies targetCurrency)
        {
            decimal exchangeRate;

            if (ExchangeRates.TryGetValue(Tuple.Create(sourceCurrency, targetCurrency), out exchangeRate))
            {
                return amount * exchangeRate;
            }
            else
            {
                throw new Exception("Exchange rate for selected currencies not found");
            }
        }

        public Currencies GetCurrenciesFromString(string currency)
        {
            Enum.TryParse(currency, out Currencies currenciesVal);

            return currenciesVal;

        }
    }

}
