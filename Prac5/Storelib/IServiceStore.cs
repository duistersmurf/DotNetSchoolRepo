using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Storelib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceStore" in both code and config file together.
    [ServiceContract]
    public interface IServiceStore
    {
        [OperationContract]
        void Login();
        [OperationContract]
        void signup();

        [DataContract]
        public class Product
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public double Price { get; set; }
            [DataMember]
            public Product(String name)
            {
                Name = name;
            }
        }

        [DataContract]
        public class Persoon
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public string Password { get; set; }
            [DataMember]
            public Persoon(String name)
            {
                Name = name;
            }
        }
    }

}
