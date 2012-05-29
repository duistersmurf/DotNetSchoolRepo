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
        Person Login();
        [OperationContract]
        bool signup();
        [OperationContract]
        Product getProductlistStore();
        [OperationContract]
        Product getProductlistPersoon();
        [OperationContract]
        Person getPerson();
        [OperationContract]
        void setProductStore();
        [OperationContract]
        void setProductPerson();
        [OperationContract]
        void setPerson();
        [OperationContract]
        void buyProduct();
        [OperationContract]
        bool searchProductPerson();
        [OperationContract]
        Product getProductPerson();
        [OperationContract]
        Product getProductStore();

        [DataContract]
        public class Product
        {
            [DataMember]
            public String ProductName { get; private set; }
            [DataMember]
            public Double ProductPrice { get; private set; }
            [DataMember]
            public Int32 AvalibleProducts { get; set; }
            [DataMember]
            public Product(String productname, Double productprice)
            {
                ProductName = productname;
                ProductPrice = productprice;
            }
        }

        [DataContract]
        public class Person
        {
            [DataMember]
            public string Name { get; set; }
            [DataMember]
            public string Password { get; set; }          
            [DataMember]
            public int Saldo { get; set; }
            [DataMember]
            public List<Product>PersonsProducts = new List<Product>();
            [DataMember]
            public Person(String name)
            {
                Name = name;
            }
        }

        [DataContract]
        public class Store
        {
            [DataMember]
            public List<Product> StoreProducts = new List<Product>();
        }
    }
}
