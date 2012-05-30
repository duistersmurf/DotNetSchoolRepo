using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

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
        public Int32 AvalibleProducts { get; set; }
        /* [DataMember]
        public Product(String productname, Double productprice)
        {
            ProductName = productname;
            ProductPrice = productprice;
        }*/
    }
}
