using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Storelib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceStore" in both code and config file together.
    public class ServiceStore : IServiceStore
    {
        public void Login()
        {
        }
        public void Signup()
        {
        }

    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }

    }
    /*
   public class Persoon
   {
       public string Name { get; set; }
       public string Password { get; set; }
   }

   public class Store
   {
       public string product { get; set;
   }*/
    }
}
