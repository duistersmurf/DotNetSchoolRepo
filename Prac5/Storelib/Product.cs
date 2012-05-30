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
        public int ProductID { get; set; }
        [DataMember]
        public String ProductName { get;  set; }
        [DataMember]
        public int ProductPrice { get; set; }
        [DataMember]
        public Int32 AvalibleProducts { get; set; }
    }
}
