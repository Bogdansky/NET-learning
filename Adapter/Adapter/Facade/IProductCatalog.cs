﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.Facade
{
    public interface IProductCatalog
    {
        Product GetProductDetails(string productId);
    }
}
