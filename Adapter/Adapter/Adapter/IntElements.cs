using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Adapter
{
    public class IntElements : IElements<int>
    {
        private readonly IEnumerable<int> _elements;
        public IntElements(IEnumerable<int> elements)
        {
            _elements = elements;
        }

        public IEnumerable<int> GetElements()
        {
            return _elements;
        }
    }
}
