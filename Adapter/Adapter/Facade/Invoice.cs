using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Facade
{
    public class Invoice : Payment
    {
        public string Email { get; set; }

        public Invoice(Payment payment, string email) : base(payment)
        {
            Email = email;
        }
    }
}
