using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagingState
{
    public class ConnacordFilter : IFilter
    {
        public IEnumerable<Trade> Match(IEnumerable<Trade> trades)
        {
            return trades.Where(t => t.Type == Type.Future && t.Amount > 10 && t.Amount < 40);
        }
    }
}
