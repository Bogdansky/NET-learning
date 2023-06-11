using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Interfaces;

namespace AlteringBehavior.Services
{
    public class KzCurrencyService : ICurrencyService
    {
        public decimal LoadCurrencyRate() => 431;
    }
}
