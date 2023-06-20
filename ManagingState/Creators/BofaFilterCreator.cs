using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingState
{
    public class BofaFilterCreator
    {
        public static IFilter CreateFilter()
        {
            return new BofaFilter();
        }
    }
}
