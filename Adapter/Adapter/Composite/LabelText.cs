using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Composite
{
    public class LabelText : ILeaf
    {
        private readonly string _value;

        public LabelText(string value)
        {
            _value = value;
        }

        public string ConvertToString()
        {
            return $"<label value='{_value}'/>";
        }
    }
}
