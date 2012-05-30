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
        Store store = new Store();
        Person person =new Person();
        int id = 0;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();
        public String test()
        {
            //store = new Store();
            String s = null;
            Person pr = new Person();
            pr.Id = 1;
            pr.Name = "kaas";
            pr.Password = "saak";
            store.StoreCustomers.Add(pr);
            foreach (Person p in store.StoreCustomers)
            {
                s = p.Name;
            }

            return s;
        }
        public void startup()
        {     
            //con.ConnectionString = @"Data Source=ruwan-flaptop\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
           // store = new Store();
            con.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from Person", con);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                Person p = new Person();
                p.Id = int.Parse(myReader["PersonID"].ToString());
                p.Name = myReader["PersonName"].ToString();
                       p.Password = myReader["PersonPassword"].ToString();
                       store.StoreCustomers.Add(p);
            }
            con.Close();

            con.Open();

            SqlDataReader myReader1 = null;
            SqlCommand myCommand1 = new SqlCommand("select * from Products", con);
            myReader1 = myCommand.ExecuteReader();
            while (myReader1.Read())
            {
                Product pt = new Product();
                //pt.ProductID = int.Parse(myReader1["ProductID"].ToString());
                pt.ProductName = myReader1["ProductName"].ToString();
                pt.ProductPrice = int.Parse(myReader1["ProductPrice"].ToString());
                pt.AvalibleProducts = int.Parse(myReader1["ProductAvalible"].ToString());
                store.StoreProducts.Add(pt);
            }
            con.Close();

            id = store.StoreCustomers.Count();                     
        }
        public bool login(String nm, String pw)
        {
            bool b = false;
            if (checkPassword(nm,pw))
            {
                foreach (Person p in store.StoreCustomers)
                {
                    if (p.Name == nm)
                    {
                        person.Id = p.Id;
                    }
                }                       
                person.Name = nm;              

                //person.PersonsProducts = 
                b = true;
            }
            return b;
        }
       public bool signup(String nm, String pw)
        {
            con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            if (searchPerson(nm))
            {
                con.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("insert into Person VALUES (" + id + "," + nm + "," + pw +")", con);
                myReader = myCommand.ExecuteReader();
                con.Close();
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
        public bool buyProduct(Product pt, int aantal)
        {
            bool b = false;

            Product storep = getProductStore(pt);
            if (searchProductPerson(pt))
            {
                Product pd = getProductPerson(pt);
                pd.AvalibleProducts = pd.AvalibleProducts + aantal;
                storep.AvalibleProducts = storep.AvalibleProducts - aantal;
                person.Saldo = person.Saldo - pd.ProductPrice;
                b = true;
            }
            else
            {
                person.PersonsProducts.Add(pt);
                person.Saldo = person.Saldo - pt.ProductPrice;
                storep.AvalibleProducts = storep.AvalibleProducts - aantal;
                b = true;
            }

            return b;
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
                if (p.Name == nm)
                {
                    if (p.Password == pw)
                    {
                        b = true;
                    }

                }
            }


                return b;
        }

        public Product getProductTestStore(String nm)
        {
            Product pt = new Product();
            foreach (Product p in store.StoreProducts)
            {
                if (p.ProductName == nm)
                    {
                        pt = p;
                         
                    }
                   
            }
            return pt;
        }
    }
}
