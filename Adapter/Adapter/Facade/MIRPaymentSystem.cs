using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Facade
{
    public class MIRPaymentSystem : IPaymentSystem
    {
        public bool MakePayment(Payment payment)
        {
            var rand = new Random().Next(0, 100);

            if (rand % 2 == 0)
            {
                Console.WriteLine($"The payment for {payment.Product.Name} was successful.");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
