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
        string startup();
        [OperationContract]
        Person login(String nm, String pw);
        [OperationContract]
        string signup(String nm);
        [OperationContract]
        bool buyProduct(Product pt, int amount, Person p);
        [OperationContract]
        int getAmountStore(int id);
        [OperationContract]
        Product getProduct(int productid);
        [OperationContract]
        List<Product> getProductListStore();
        [OperationContract]
        List<Product> getProductListPerson(int personid);
        [OperationContract]
        int getSaldoPerson(int pid);
     //   [OperationContract]
     //   Product getProductStore(Product pt);
     //   [OperationContract]
    //    bool searchPerson(String nm);
   //     [OperationContract]
    //    bool checkPassword(String nm, String pw);
    //    [OperationContract]
   //     Product searchProductStore(String nm);*/
    }
 }
