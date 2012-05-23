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

      

  
    }
}
