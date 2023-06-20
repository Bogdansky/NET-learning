using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingState
{
    public class ConnacordFilterCreator
    {
        public static IFilter CreateFilter()
        {
            return new ConnacordFilter();
        }
    }
}
