using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Storelib;

namespace Storelib
{
    [Serializable]
    class Product
    {
        
        public String ProductName { get; private set; }
        public Double ProductPrice { get; private set; }
        public int AvalibleProducts { get; set; }

        public Product(String productname, Double productprice)
        {
            ProductName = productname;
            ProductPrice = productprice;
        }
    }
}
