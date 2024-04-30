using Greggs.Products.Api.Models;

namespace Greggs.Products.Api.Services.CurrencyConverter
{
    public interface ICurrencyConverter
    {
        Currencies GetCurrenciesFromString(string currency);
        decimal ConvertCurrency(decimal amount, string sourceCurrencyCode, string targetCurrencyCode);
        decimal ConvertCurrency(decimal amount, Currencies sourceCurrency, Currencies targetCurrency);
    }
}
