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
        int id;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();

        public void startup()
        {     
            con.ConnectionString = @"Data Source=ruwan-flaptop\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            //con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
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
            myReader1 = myCommand1.ExecuteReader();
            while (myReader1.Read())
            {
                Product pt = new Product();
                pt.ProductID = int.Parse(myReader1["ProductID"].ToString());
                pt.ProductName = myReader1["ProductName"].ToString().Trim();
                pt.ProductPrice = int.Parse(myReader1["ProductPrice"].ToString().Trim());
                pt.AvalibleProducts = int.Parse(myReader1["ProductAvalible"].ToString().Trim());
                store.StoreProducts.Add(pt);
            }
            con.Close();

            id = store.StoreCustomers.Count();
            Console.WriteLine(id);      
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
            con.ConnectionString = @"Data Source=ruwan-flaptop\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            //con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            if (searchPerson(nm))
            {
                try
                {                    
                    con.Open();
                    int idt = id+1;
                    Console.WriteLine(idt + " " + nm + " " + pw);
                    Console.WriteLine("insert into Person (PersonID, PersonName, PersonPassword) VALUES (" + 3 + "," + nm + "," + pw + ")");
                    SqlCommand myCommand = new SqlCommand("INSERT INTO Person (PersonID, PersonName, PersonPassword) VALUES (" + 3 + "," + nm + "," + pw + ")", con);                   
                    myCommand.ExecuteNonQuery();
                   // SqlCommand mySqlCommand = con.CreateCommand();
                    //mySqlCommand.CommandText = "INSERT INTO Person (PersonID, PersonName, PersonPassword) VALUES (" + 3 + "," + nm + "," + pw + ")";
                    con.Close();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return false;
        }
       public  void setProductStore(String nm, int pr)
        {
            Product pd = new Product();
            store.StoreProducts.Add(pd);
        }
        public void setPerson(string nm, string pw)
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

            Product storep = pt;
            Console.WriteLine(pt.ProductName);
            if (searchProductPerson(pt))
            {
                Product pd = getProductPerson(pt);
                pd.AvalibleProducts = pd.AvalibleProducts + aantal;
                storep.AvalibleProducts = storep.AvalibleProducts - aantal;
                person.Saldo = person.Saldo - pd.ProductPrice;
                Console.WriteLine("searcHproduct is true");
                b = true;
                
                foreach (Product ptd in person.PersonsProducts)
                {
                    Console.WriteLine(ptd.ProductName);
                    Console.WriteLine(ptd.AvalibleProducts);
                }
            }
            else
            {
                Product prd = new Product();
                prd.ProductName = pt.ProductName;
                //prd.ProductID = pID;
                prd.AvalibleProducts = aantal;
                person.PersonsProducts.Add(prd);
                person.Saldo = person.Saldo - pt.ProductPrice;
                storep.AvalibleProducts = storep.AvalibleProducts - aantal;
                Console.WriteLine("searcHproduct is false");
                b = true;
                
                foreach (Product ptd in person.PersonsProducts)
                {
                    Console.WriteLine(ptd.ProductName);
                    Console.WriteLine(ptd.AvalibleProducts + "k");
                }

            }

            return b;
        }
        public bool searchProductPerson(Product pt)
        {
            bool b = false;

            foreach (Product pr in person.PersonsProducts)
            {
                if (pt.ProductName.Trim() == pr.ProductName.Trim())
                {
                    b = true;

                }          
            }

            return b;
        }
        public Product getProductPerson(Product pt)
        {
            Product pd = pt;

            Console.WriteLine(pt.ProductName + "get productperson");
            foreach (Product pr in person.PersonsProducts)
            {
                Console.WriteLine("foreach getproductperson");
                if (pt.ProductName.Trim() == pr.ProductName.Trim())
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

            bool b = true;

            con.ConnectionString = @"Data Source=ruwan-flaptop\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            //con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            // store = new Store();
            con.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from Person", con);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                if (myReader["PersonName"].ToString().Trim().Equals(nm))
                {                   
                        b = true;
                }

            }
            con.Close();

            return b;       
        }
        public bool checkPassword(string nm, string pw)
        {
            bool b = false;

            con.ConnectionString = @"Data Source=ruwan-flaptop\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            //con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            // store = new Store();
            con.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from Person", con);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {              
                if (myReader["PersonName"].ToString().Trim().Equals(nm))
                {
                    if (myReader["PersonPassword"].ToString().Trim() == pw)
                    {
                        b = true;
                    }
                }

            }
            con.Close();

                return b;
        }

        public Product getProductTestStore(String nm)
        {
            Product pt = new Product();
            foreach (Product p in store.StoreProducts)
            {
                Console.WriteLine(p.ProductName + "get product test store" );
                Console.WriteLine(nm + "get product test store");
                if (p.ProductName == nm)
                    {
                        pt = p;
                         
                    }
                   
            }
            return pt;
        }
    }
}
