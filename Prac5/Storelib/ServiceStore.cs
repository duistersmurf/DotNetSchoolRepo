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
        Store store;
        Person person;
        Person Login(String nm, String pw)
        {            
            if (true)
            {
                store = new Store();
                person = new Person("Henk", "test");
            }
            return person;
        }
        bool Signup(String nm, String pw)
        {
            if (true)
            {
                setPerson(nm, pw);
            }
            return false;
        }
        void setProductStore(String nm, int pr)
        {
            Product pd = new Product(nm, pr);
            store.StoreProducts.Add(pd);
        }
        void setPerson(String nm, String pw)
        {
           Person p =  new Person(nm, pw);
           store.StoreCustomers.Add(p);

        }
        public List<Product> getProductlistStore()
        {
            return store.StoreProducts;
        }
        public List<Product> getProductlistPersoon()
        {
            return person.PersonsProducts;
        }

        public List<Person> getPerson()
        {
            return store.StoreCustomers;           
        }
        public void buyProduct(Product pt, int aantal)
        {
            Product storep = getProductStore(pt);
            if (searchProductPerson(pt))
            {
                Product pd = getProductPerson(pt);
                pd.AvalibleProducts = pd.AvalibleProducts + aantal;
                storep.AvalibleProducts = storep.AvalibleProducts - aantal;
                person.Saldo = person.Saldo - pd.ProductPrice;
            }
            else
            {
                person.PersonsProducts.Add(pt);
                person.Saldo = person.Saldo - pt.ProductPrice;
                storep.AvalibleProducts = storep.AvalibleProducts - aantal;
            }

        }
        public bool searchProductPerson(Product pt)
        {
            bool b = false;

            foreach (Product pr in person.PersonsProducts)
            {
                if (pt.ProductName == pr.ProductName)
                {
                    b = true;

                }          
            }

            return b;
        }
        public Product getProductPerson(Product pt)
        {
            Product pd = pt;

            foreach (Product pr in person.PersonsProducts)
            {
                if (pd.ProductName == pr.ProductName)
                {
                    pd = pr;

                }
            }

            return pd;
        }
        public Product getProductStore(Product pt)
        {
            Product pd = pt;

            foreach (Product pr in store.StoreProducts)
            {
                if (pd.ProductName == pr.ProductName)
                {
                    pd = pr;

                }
            }
            return pd;
        }
    }
}
