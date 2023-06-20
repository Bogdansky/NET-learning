using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Adapter
{
    public class Adapter<T> : IContainer<T>
    {
        private readonly IElements<T> _elements;

        public IEnumerable<T> Items => _elements.GetElements();
        public int Count => _elements.GetElements().Count();

        public Adapter(IElements<T> elements)
        {
            _elements = elements;
        }
    }
}
