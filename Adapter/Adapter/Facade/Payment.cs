using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Facade
{
    public class Payment
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Amount => Product.Price * Quantity;

        public Payment(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Payment(Payment payment)
        {
            Product = payment.Product;
            Quantity = payment.Quantity;
        }
    }
}
