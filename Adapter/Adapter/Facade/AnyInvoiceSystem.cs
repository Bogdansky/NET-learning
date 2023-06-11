using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Facade
{
    public class AnyInvoiceSystem : IInvoiceSystem
    {
        public void SendInvoice(Invoice invoice)
        {
            var randValue = new Random().NextDouble();

            if (randValue > 1 / 2)
            {
                Console.WriteLine($"The invoice for {invoice.Product.Name} with amount {invoice.Amount} was sent successfully");
            }
            else
            {
                throw new Exception("The invoice was not sent due to unknown reason!");
            }
        }
    }
}
