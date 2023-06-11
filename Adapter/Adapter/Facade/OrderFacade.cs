using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Facade
{
    public class OrderFacade
    {
        private readonly IProductCatalog _productCatalog;
        private readonly IPaymentSystem _paymentSystem;
        private readonly IInvoiceSystem _invoiceSystem;

        public OrderFacade(IProductCatalog productCatalog,
                           IPaymentSystem paymentSystem,
                           IInvoiceSystem invoiceSystem)
        {
            _productCatalog = productCatalog;
            _paymentSystem = paymentSystem;
            _invoiceSystem = invoiceSystem;
        }

        public void PlaceOrder(string productId, int quantity, string email)
        {
            var product = _productCatalog.GetProductDetails(productId);
            if (product == null) throw new ArgumentException($"The product with {productId} productId does not exist!");

            var payment = new Payment(product, quantity);

            var paymentResult = _paymentSystem.MakePayment(payment);
            if (!paymentResult) throw new Exception("The payment was not successful!");

            try
            {
                _invoiceSystem.SendInvoice(new Invoice(payment, email));
            }
            catch
            {
                throw;
            }
        }
    }
}
