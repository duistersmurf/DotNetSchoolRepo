using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Storelib
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public String ProductName { get; private set; }
        [DataMember]
        public Double ProductPrice { get; private set; }
        [DataMember]
        public Product(String productname, Double productprice)
        {
            ProductName = productname;
            ProductPrice = productprice;
        }
    }
}
