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
        bool Login();
        [OperationContract]
        bool signup();
        [OperationContract]
        Product getProduct();
        [OperationContract]
        Persoon getPersoon();
        [OperationContract]
        void setProduct();
        [OperationContract]
        void setPersoon();
        /*[OperationContract]
        void setProductPrice();
        [OperationContract]
        void setProductName();
        [OperationContract]
        void setName();
        [OperationContract]
        void setPassword();*/

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
