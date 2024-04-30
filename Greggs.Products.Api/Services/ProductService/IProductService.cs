using Greggs.Products.Api.Models;
using System.Collections.Generic;

namespace Greggs.Products.Api.Services.ProductService
{
    public interface IProductService
    {
        public IEnumerable<Product> ProductList(int pageStart, int pageSize);
    }
}
