using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Greggs.Products.Api.Services;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    private readonly ILogger<ProductController> _logger;
    private readonly ICurrencyConverter _currencyConverter;

    public ProductController(ILogger<ProductController> logger,ICurrencyConverter currencyConverter)
    {
        _logger = logger;
        _currencyConverter = currencyConverter;
    }

    [HttpGet]
    public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5, string currency = "GBP")
    {
        var list = new ProductAccess().List(pageStart, pageSize).ToArray();

        if (currency == "GBP")
        {
            return list;
        }
        else if (currency == "EUR")
        {
            foreach (var item in list)
            {
                item.Price = _currencyConverter.ConvertCurrency(item.Price, Currencies.GBP, Currencies.EUR);
            }
        }
        else
        {
            return null;
        }
        return list;
    }
}