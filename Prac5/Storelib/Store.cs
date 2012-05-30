using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Storelib
{
    [DataContract]
    public class Store
    {
        [DataMember]
        public List<Product> StoreProducts = new List<Product>();
        [DataMember]
        public List<Person> StoreCustomers = new List<Person>();
    }
}
