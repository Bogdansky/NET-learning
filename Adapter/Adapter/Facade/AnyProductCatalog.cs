using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Facade
{
    public class AnyProductCatalog : IProductCatalog
    {
        private readonly Dictionary<string, Product> _products;

        public AnyProductCatalog(Dictionary<string, Product> products)
        {
            _products = products;
        }

        public Product GetProductDetails(string productId)
        {
            return _products[productId];
        }
    }
}
