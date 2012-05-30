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
        void startup();
        [OperationContract]
        Person login(String nm, String pw);
        [OperationContract]
        bool signup(String nm, String pw);
        [OperationContract]
        void setProductStore(String nm, int pr);
        [OperationContract]
        void setPerson(String nm, String pw);
        [OperationContract]
        List<Product> getProductlistStore();
        [OperationContract]
        List<Product> getProductlistPersoon();
        [OperationContract]
        List<Person> getPerson();
        [OperationContract]
        void buyProduct(Product pt, int aantal);
        //[OperationContract]
        //void setProductPerson();
        [OperationContract]
        bool searchProductPerson(Product pt);
        [OperationContract]
        Product getProductPerson(Product pt);
        [OperationContract]
        Product getProductStore(Product pt);
        [OperationContract]
        bool searchPerson(String nm);
        [OperationContract]
        bool checkPassword(String nm, String pw);
    }
 }
