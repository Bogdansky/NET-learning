using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingState
{
    public class TradeFilter
    {
        public IEnumerable<Trade> FilterForBank(IEnumerable<Trade> trade, Bank bank)
        {
            var filter = BankFilterFactory.CreateFilter(bank);
            return filter.Match(trade);
        }
    }
}
