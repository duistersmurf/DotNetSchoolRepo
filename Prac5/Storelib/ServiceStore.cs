using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Storelib;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace Storelib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceStore" in both code and config file together.
    public class ServiceStore : IServiceStore
    {
        Store store = new Store();
        
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();

        public void startup()
        {     
            //con.ConnectionString = @"Data Source=ruwan-flaptop\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";          
           con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";          
           con.Open();
           Console.WriteLine("startup test1");
            SqlDataReader myReader = null;                    
            SqlCommand myCommand = new SqlCommand("select * from Person", con);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                Console.WriteLine("startup test2");
                Person p = new Person();
                p.Id = int.Parse(myReader["PersonID"].ToString().Trim());
                p.Name = myReader["PersonName"].ToString().Trim();
                p.Password = myReader["PersonPassword"].ToString().Trim();
                p.Saldo = int.Parse(myReader["PersonSaldo"].ToString().Trim());
                Console.WriteLine("startup test3");
                Console.WriteLine("startup test3.1" + p.Name);
               store.StoreCustomers.Add(p);
            }
           con.Close();

           foreach (Person p in store.StoreCustomers)
           {
               Console.WriteLine("startup test4");
               Console.WriteLine("startup test4" + p.Name);
               p.PersonsProducts = setPersonProduct(p);
               Console.WriteLine("startup test5");
           }

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

            

           

            foreach (Person p in store.StoreCustomers)
            {
                foreach (Product pt in p.PersonsProducts)
                {
                   Console.WriteLine(pt.ProductName + p.Name);
                }
            }
        }
        public Person login(String nm, String pw)
        {

            Person personlogin = new Person();
            if (checkPassword(nm,pw))
            {
                foreach (Person p in store.StoreCustomers)
                {
                    if (p.Name == nm)
                    {
                        personlogin.Id = p.Id;
                        personlogin.Name = p.Name;
                       // personlogin.PersonsProducts = p
                        break;
                    }
                }                                    

                //person.PersonsProducts = 
            }
            return personlogin;
        }
       public bool signup(String nm, String pw)
        {
            bool b = false;
            int idcount = 0;
            Console.WriteLine(nm + pw);
            con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            con.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select count(*) from Person", con);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                //idcount = int.Parse(myReader.ToString());
                idcount = myReader.GetInt32(0);
            }
            con.Close();

            Console.WriteLine("signup" + idcount);

            if (searchPerson(nm))
            {
                try
                {
                    SqlConnection sqlConn = new SqlConnection(@"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True");
                    int newid = idcount++;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "INSERT INTO Person (PersonID, PersonName, PersonPassword) VALUES ('" + newid + "','" + nm + "','" + pw + "')";
                    cmd.Connection = sqlConn;

                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                    sqlConn.Close();
                    b = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    b = false;
                }
            }
            else
            {
                b = false;
            }
            return b;       
        }
       public  void setProductStore(String nm, int pr)
        {
            Product pd = new Product();
            store.StoreProducts.Add(pd);
        }
       public List<Product> setPersonProduct(Person p)
        {
            List<Product> products = new List<Product>();
            Console.WriteLine("setPersonProduct" + p.Name);
            List<int> productsids = getJProductID(p);
            try
            {
                foreach (int i in productsids)
                {
                    con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
                    con.Open();
                    SqlDataReader myReader3 = null;
                    SqlCommand myCommand3 = new SqlCommand("select * from Products where ProductID =" + i, con);
                    myReader3 = myCommand3.ExecuteReader();
                    while (myReader3.Read())
                    {
                        Console.WriteLine("setPersonProdcut test4");
                        Product pt = new Product();
                        pt.ProductID = int.Parse(myReader3["ProductID"].ToString().Trim());
                        pt.ProductName = myReader3["ProductName"].ToString().Trim();
                        pt.ProductPrice = int.Parse(myReader3["ProductPrice"].ToString().Trim());
                        pt.AvalibleProducts = int.Parse(myReader3["ProductAvalible"].ToString().Trim());
                        products.Add(pt);
                    }
                    con.Close();
                }
            }
           catch(Exception e)
            {
                Console.WriteLine("setPersonProduct" + e.Message);

           }
            return products;
        }
       public List<int> getJProductID(Person p)
       {
           List<int> id = new List<int>(); 
           con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
           con.Open();
           try
           {
               SqlDataReader myReader2 = null;
               Console.WriteLine("getJProductID test2.1 " + p.Id);
               SqlCommand myCommand2 = new SqlCommand("select * from JoinProducts where PersonID =" + p.Id, con);
               Console.WriteLine("getJProductID test2.1.1 " + "select * from JoinProducts where PersonID = " + p.Id);
               Console.WriteLine("getJProductID test2.2");
               myReader2 = myCommand2.ExecuteReader();
               while (myReader2.Read())
               {
                   Console.WriteLine("getJProductID test2.3 " + myReader2["ProductID"].ToString().Trim());
                   int idt = int.Parse(myReader2["ProductID"].ToString().Trim());
                   id.Add(idt);

               }
           }
           catch (Exception e)
           {
               Console.WriteLine("getJproductid\n"+e.Message);
           }
           con.Close();

           return id;
       }
        /*public bool buyProduct(Product pt, int aantal)
        {
            bool b = false;

            Product storep = pt;
            Console.WriteLine(pt.ProductName);
         //als persoon product heeft, aantal updaten met het gekochten aantal
         // en bij de store het product aantal verlagen
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
         // persoon heeft het product nog niet , insert stmt nieuwe aanmaken met tabeltotaal + 1 als id
         // bij insert het aantal mee geven het aantal van 
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
        }*/
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

            //con.ConnectionString = @"Data Source=ruwan-flaptop\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            con.ConnectionString = @"Data Source=duistersmurf-pc\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";
            // store = new Store();
            con.Open();

            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from Person", con);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                if (myReader["PersonName"].ToString().Trim().Equals(nm))
                {
                    b = false;
                    break;
                }
                else
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

        public Product searchProductStore(String nm)
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
