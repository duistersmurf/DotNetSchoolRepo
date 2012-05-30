using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Storelib;
using System.Data.SqlClient;
using System.Data;

namespace Storelib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceStore" in both code and config file together.
    public class ServiceStore : IServiceStore
    {
        Store store;
        Person person;
        int id = 0;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();
        public void startup()
        {
            con.ConnectionString = @"Data Source=ruwan-flaptop\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            store = new Store();
            con.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from Person", con);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                Person p = new Person();
                p.Name = myReader["PersonName"].ToString();
                       p.Password = myReader["PersonPassword"].ToString();
                       store.StoreCustomers.Add(p);
            }
            con.Close();

            id = store.StoreCustomers.Count() + 1;

        }
        public Person login(String nm, String pw)
        {            
            if (checkPassword(nm,pw))
            {
                
                person = new Person();
                person.Name = nm;

                //person.PersonsProducts = 
            }
            return person;
        }
       public bool signup(String nm, String pw)
        {
            if (searchPerson(nm))
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("insert into Person VALUES (" + id + "," + nm + "," + pw +")", con);
                myReader = myCommand.ExecuteReader();
            }
            return false;
        }
       public  void setProductStore(String nm, int pr)
        {
            Product pd = new Product();
            store.StoreProducts.Add(pd);
        }
        public void setPerson(String nm, String pw)
        {
           Person p =  new Person();
           p.Name = nm;
           p.Password = pw;
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
        public bool searchPerson(String nm)
        {
            bool b = false;

            foreach (Person pr in store.StoreCustomers)
            {
                if (!(nm == pr.Name))
                {
                    b = true;

                }
            }

            return b;
        }
        public bool checkPassword(String nm, String pw)
        {
            bool b = false;
            
            foreach(Person p in store.StoreCustomers)
            {
                if (nm == p.Name)
                {
                    if (pw == p.Password)
                    {
                        b = true;
                    }

                }
            }


                return b;
        }
    }
}
