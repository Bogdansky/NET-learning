using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Adapter
{
    public interface IElements<T>
    {
        IEnumerable<T> GetElements();
    }
}
