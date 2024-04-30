using System;
using System.Collections.Generic;
using System.Linq;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Greggs.Products.Api.Services.CurrencyConverter;
using Greggs.Products.Api.Services.ProductService;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    private readonly ILogger<ProductController> _logger;
    private readonly ICurrencyConverter _currencyConverter;
    private readonly IProductService _productService;
    

    public ProductController(ILogger<ProductController> logger,ICurrencyConverter currencyConverter,IProductService productService)
    {
        _logger = logger;
        _currencyConverter = currencyConverter;
        _productService = productService;
    }

    [HttpGet]
    public IEnumerable<Product> Get(int pageStart = 0, int pageSize = 5, string currency = "GBP")
    {
        var list = _productService.ProductList(pageStart, pageSize).ToArray();

        if (currency == "GBP")
        {
           return setCurrencyFieldGBP(list);
        }
        else if (currency == "EUR")
        {
            return DoGBPtoEURConversion(list);
        }
        else
        {
            return null;
        }
    }

    private Product[] setCurrencyFieldGBP(Product[] list)
    {
        foreach (var item in list)
        {
            item.Currency = "GBP";
        }
        return list;
    }

    private Product[] DoGBPtoEURConversion(Product[] list)
    {
        foreach (var item in list)
        {
            item.Price = _currencyConverter.ConvertCurrency(item.Price, Currencies.GBP, Currencies.EUR);
            item.Currency = "EUR";
        }
        return list;
    }
}