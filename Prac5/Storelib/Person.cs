using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Storelib
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }  
        [DataMember]
        public string Password { get; set; }    
        [DataMember]
        public double Saldo { get; set; }
        [DataMember]
        public List<Product> PersonsProducts = new List<Product>();
    }
}
