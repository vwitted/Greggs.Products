using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Greggs.Products.Api.Services.ProductService
{

    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IDataAccess<Product> _productAccess;

        public ProductService(ILogger<ProductService> logger, IDataAccess<Product> productAccess)
        {
            _logger = logger;
            _productAccess = productAccess;
        }

        public IEnumerable<Product> ProductList(int pageStart, int pageSize)
        {
            return _productAccess.List(pageStart, pageSize);
        }
    }

}
