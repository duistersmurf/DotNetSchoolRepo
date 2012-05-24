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
        public bool Login()
        {
            return false;
        }
        public bool Signup()
        {
            return false;
        }
      /*  void setProduct()
        {
        }
        void setPersoon()
        {
        }
        void setProductPrice()
        {
        }
        void setProductName()
        {
        }
        void setName()
        {
        }
        void setPassword()
        {
        }*/

        public List<Product> GetProducts()
        {
            return new List<Product> { new Product("iPad", 20), new Product("iPod", 200) };
        }

        public List<Persoon> getPersoon()
        {

            return new List<Persoon> { new Persoon("hewnk"), new Persoon("nmewaton") };
        }
    }
}
