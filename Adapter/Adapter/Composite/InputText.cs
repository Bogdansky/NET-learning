﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Composite
{
    public class InputText : ILeaf
    {
        private readonly string _name;
        private readonly string _value;

        public InputText(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string ConvertToString()
        {
            return $"<inputText name='{_name}' value='{_value}'/>";
        }
    }
}
