using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingState
{
    public class BarclaysFilterCreator
    {
        public static IFilter CreateFilter()
        {
            return new BarclaysFilter();
        }
    }
}
