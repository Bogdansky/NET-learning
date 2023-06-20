using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingState
{
    public class BankFilterFactory
    {
        public static IFilter CreateFilter(Bank bank)
        {
            return bank switch
            {
                Bank.Bofa => BofaFilterCreator.CreateFilter(),
                Bank.Barclays => BarclaysFilterCreator.CreateFilter(),
                Bank.Connacord => ConnacordFilterCreator.CreateFilter(),
                _ => throw new NotImplementedException("Unsupported bank"),
            };
        }
    }
}
