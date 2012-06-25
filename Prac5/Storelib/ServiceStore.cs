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
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Storelib
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceStore" in both code and config file together.
    public class ServiceStore : IServiceStore
    {
        Store store = new Store();
        
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection();

        public string startup()
        {
            string s = "connected";

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );
                OracleConnection conn = new OracleConnection(connectionString);
                try
                {
                    conn.Open();

                    OracleCommand cmd3 = conn.CreateCommand();
                    cmd3.CommandText = "select * from PERSON";
                    OracleDataReader myReader = cmd3.ExecuteReader();

                    while (myReader.Read())
                    {
                        if (myReader["NAME"].ToString().Trim().Trim().Equals("Store"))
                        {
                            s += " and database connection oke";
                        }
                    }
                    conn.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("startup error" + e.Message);
                    s = "database error";
                }

            return s;
        }

        public Person login(String nm, String pw)
        {
            Person personlogin = new Person();
            if (checkPassword(nm, pw))
            {

                String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );
                OracleConnection conn = new OracleConnection(connectionString);
                try
                {
                    conn.Open();
                    OracleCommand cmd3 = conn.CreateCommand();
                    cmd3.CommandText = "select * from Person where NAME = '" + nm + "'";
                    OracleDataReader myReader = cmd3.ExecuteReader();

                    while (myReader.Read())
                    {
                        personlogin.Id = int.Parse(myReader["PERSON_ID"].ToString().Trim());
                        personlogin.Name = myReader["NAME"].ToString().Trim();
                        personlogin.Password = myReader["PASSWORD"].ToString().Trim();
                        personlogin.Saldo = int.Parse(myReader["SALDO"].ToString().Trim());
                    }
                    conn.Close();
                    personlogin.PersonsProducts = setPersonProduct(personlogin.Id);
                }
                catch (Exception e)
                {
                    Console.WriteLine("login error" + e.Message);
                }
            }
            else
            {
                personlogin = null;
            }
            return personlogin;
        }

        public bool checkPassword(string nm, string pw)
        {
            bool b = false;
            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );

            OracleConnection conn = new OracleConnection(connectionString);
            try
            {
                
                conn.Open();
         
                OracleCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select * from PERSON";
                OracleDataReader myReader = cmd3.ExecuteReader();

                while (myReader.Read())
                {
                    if (myReader["NAME"].ToString().Trim().Equals(nm))
                    {
                        if (myReader["PASSWORD"].ToString().Trim() == pw)
                        {
                            b = true;
                        }
                    }

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("checkpassword " + e.Message);
            }

            return b;
        }

        public List<Product> setPersonProduct(int pid)
        {
            List<Product> pdlist = new List<Product>();
            List<int> prdtids = getProductids(pid);
            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );
                OracleConnection conn = new OracleConnection(connectionString);
                try
                {
                    foreach (int i in prdtids)
                    {
                        conn.Open();

                        OracleCommand cmd3 = conn.CreateCommand();
                        cmd3.CommandText = "select * from PRODUCT where PRODUCT_ID =" + i;
                        OracleDataReader myReader = cmd3.ExecuteReader();

                        while (myReader.Read())
                        {
                            Product pr = new Product();
                            pr.ProductID = int.Parse(myReader["PRODUCT_ID"].ToString().Trim());
                            pr.ProductName = myReader["PRODUCT_NAME"].ToString().Trim();
                            pr.ProductPrice = int.Parse(myReader["PRODUCT_PRICE"].ToString().Trim());
                            pdlist.Add(pr);

                        }
                        conn.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("setpersonproducts error" + e.Message);
                }

            return pdlist;
        }

        public List<int> getProductids(int pid)
        {
            List<int> productsids = new List<int>();

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );
            OracleConnection conn = new OracleConnection(connectionString);
            try
            {
                conn.Open();

                OracleCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select * from JOIN_PRODUCTS_PERSON where PERSON_ID =" + pid;
                OracleDataReader myReader = cmd3.ExecuteReader();

                while (myReader.Read())
                {
                    int i;
                    i = int.Parse(myReader["PRODUCT_ID"].ToString().Trim());
                    productsids.Add(i);
                 
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("setpersonproducts error" + e.Message);
            }


            return productsids;
            
        }

        public string signup(string nm)
        {
              string pw = null;
              String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );
              if (searchPerson(nm))
              {
                  try
                  {

                      pw = passwordgen(nm);
                      OracleConnection conn = new OracleConnection(connectionString);
                      conn.Open();
                      OracleCommand cmd1 = conn.CreateCommand();
                      cmd1.CommandText = "INSERT INTO Person (NAME, PASSWORD,SALDO) VALUES ('" + nm + "','" + pw + "', 1000)";
                      cmd1.ExecuteNonQuery();
                      conn.Close();
                  }
                  catch (Exception e)
                  {
                      Console.WriteLine("signup \n" + e.Message);
                  }
              }
              else
              {
                  pw = "ongeldige gebruikersname";
              }
            return pw;
        }

        public bool searchPerson(String nm)
        {

            bool b = true;

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();

                OracleCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select * From PERSON";
                OracleDataReader reader = cmd3.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["NAME"].ToString().Trim() == null)
                    {
                        b = true;
                    }
                    if (reader["NAME"].ToString().Trim().Equals(nm))
                    {
                        b = false;
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("searchPerson" + e.Message);
                b = false;
            }

            return b;
        } 

        public bool buyProduct(Product pt, int amount, Person p)
        {
            bool b = false;
            int nwamountstore;
            int nwamountperson;
            int nwsaldoperson;
            int nwsaldostore;
            int price = pt.ProductPrice * amount;
            if (checkStoreAmount(pt.ProductID, amount))
            {
                if (getSaldoPerson(p.Id) >= price)
                {
                    if (searchProductPerson(pt.ProductID, p.Id))
                    {
                        nwsaldoperson = getSaldoPerson(p.Id) - price;
                        nwsaldostore = getSaldoStore() + price;
                        nwamountstore = getAmountStore(pt.ProductID) - amount;
                        nwamountperson = getAmountPerson(p.Id, pt.ProductID) + amount;
                        buyUProductPerson(nwamountperson, p.Id, pt.ProductID, nwsaldoperson);
                        buyUProductStore(nwamountstore, 1, pt.ProductID,nwsaldostore);
                        b = true;
                    }
                    else
                    {
                        nwsaldoperson = getSaldoPerson(p.Id) - price;
                        nwsaldostore = getSaldoStore() + price;
                        nwamountstore = getAmountStore(pt.ProductID) - amount;
                        buyIProductPerson(amount, p.Id, pt.ProductID, nwsaldoperson);
                        buyUProductStore(nwamountstore, 1, pt.ProductID,nwsaldostore);
                        b = true;
                    }
                }
                else
                {
                    b = false;
                }
            }
            else
            {
                b = false;
                Console.WriteLine("store amount is false");
            }

            return b;
        }
        
        public bool searchProductPerson(int ptid, int pid)
        {
            bool b = false;

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();

                OracleCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select PRODUCT_ID from JOIN_PRODUCTS_PERSON where PERSON_ID = " + pid;
                OracleDataReader myReader = cmd3.ExecuteReader();

                while (myReader.Read())
                {
                    if(int.Parse(myReader["PRODUCT_ID"].ToString().Trim()) == ptid)
                    {
                        b = true;
                    }

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("getAmountStore" + e.Message);
            } 

            return b;
        }

        public bool buyUProductPerson(int nwamount, int personid, int productid, int nwsaldop)
        {
            bool b = false;
            if (setUAmountPerson(nwamount, personid, productid) && setSaldoPerson(nwsaldop, personid))
            {
                b = true;
            }
            return b;
        }

        public bool buyUProductStore(int nwamount, int storeid, int productid, int nwsaldos)
        {
            bool b = false;

            if (setAmountStore(nwamount, storeid, productid) && setSaldoStore(nwsaldos, storeid))
            {
                b = true;
            }
            

            return b;
        }

        public bool buyIProductPerson(int amount, int personid, int productid, int nwsaldop)
        {
            bool b = false;

            if (setIAmountPerson(amount, personid, productid) && setSaldoPerson(nwsaldop, personid))
            {
                b = true;
            }

            return b;
        }

        public bool checkStoreAmount(int productid,int buyAmount)
        {
            bool b = false;
            int storeamount;
            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );
            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();
                OracleCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select Amount from JOIN_PRODUCTS_PERSON where PERSON_ID  = 1 and PRODUCT_ID = " + productid;
                OracleDataReader myReader = cmd3.ExecuteReader();
                while (myReader.Read())
                {
                    storeamount = int.Parse(myReader["Amount"].ToString().Trim());
                    if (storeamount >= buyAmount)
                    {
                        b = true;
                        break;
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("checkstoreamount \n" + e.Message);
            }
            

            return b;
        }

        public int getAmountStore(int id)
        {
            int storeamount = -1;

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();

                OracleCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select Amount from JOIN_PRODUCTS_PERSON where PERSON_ID = 1 and PRODUCT_ID = " + id;
                OracleDataReader myReader = cmd3.ExecuteReader();

                while (myReader.Read())
                {
                    storeamount = int.Parse(myReader["Amount"].ToString().Trim());

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("getAmountStore" + e.Message);
            }       
      
            return storeamount;
        }

        public int getAmountPerson(int pid,int ptid)
        {
            int storeamount = -1;
            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();

                OracleCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select Amount from JOIN_PRODUCTS_PERSON where PERSON_ID = " + pid + " and PRODUCT_ID = " + ptid;
                OracleDataReader myReader = cmd3.ExecuteReader();

                while (myReader.Read())
                {
                    storeamount = int.Parse(myReader["Amount"].ToString().Trim());

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("getamountperson error \n" + e.Message);
            }
              
      
            return storeamount;
        }

        public int getSaldoPerson(int pid)
        {
            int personsaldo = -1;

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();

                OracleCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select SALDO from PERSON where PERSON_ID = " + pid;
                OracleDataReader myReader = cmd3.ExecuteReader();

                while (myReader.Read())
                {
                    personsaldo = int.Parse(myReader["SALDO"].ToString().Trim());

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("getPersonStore" + e.Message);
            }

            return personsaldo;
 
        }

        public int getSaldoStore()
        {
            int storesaldo = -1;

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();

                OracleCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select SALDO from PERSON where PERSON_ID = 1";
                OracleDataReader myReader = cmd3.ExecuteReader();

                while (myReader.Read())
                {
                    storesaldo = int.Parse(myReader["SALDO"].ToString().Trim());

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("getsaldoStore" + e.Message);
            }

            return storesaldo;
        }

        public bool setAmountStore(int nwamount, int storeid, int productid)
        {
            bool b = false;

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
               "stud1529990", // hier je username, 
               "stud1529990"  // hier je password
               );
            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();
                OracleCommand cmd1 = conn.CreateCommand();
                cmd1.CommandText = "UPDATE JOIN_PRODUCTS_PERSON SET AMOUNT =" + nwamount + " WHERE PERSON_ID = " + storeid + " AND PRODUCT_ID = " + productid;
                cmd1.ExecuteNonQuery();
                b = true;
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("setAmountStore \n" + e.Message);
            }

            return b;

        }

        public bool setUAmountPerson(int nwamount, int personid, int productid)
        {
            bool b = false;

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
               "stud1529990", // hier je username, 
               "stud1529990"  // hier je password
               );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();
                OracleCommand cmd1 = conn.CreateCommand();
                cmd1.CommandText = "UPDATE JOIN_PRODUCTS_PERSON SET AMOUNT = " + nwamount + " WHERE PERSON_ID = " + personid + " AND PRODUCT_ID = " + productid;
                cmd1.ExecuteNonQuery();
                b = true;
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("setUAmountPerson \n" + e.Message);
            }

            return b;
        }

        public bool setIAmountPerson(int amount, int personid, int productid)
        {
            bool b = false;

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
              "stud1529990", // hier je username, 
              "stud1529990"  // hier je password
              );
            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();
                OracleCommand cmd1 = conn.CreateCommand();
                cmd1.CommandText = "insert into JOIN_PRODUCTS_PERSON (AMOUNT,PERSON_ID,PRODUCT_ID) values (" + amount + "," + personid + "," + productid + ")";
                cmd1.ExecuteNonQuery();
                b = true;
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("setIAmountPerson \n" + e.Message);
            }

            return b;
        }

        public bool setSaldoPerson(int nwsaldo, int personid)
        {
            bool b = false;

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
               "stud1529990", // hier je username, 
               "stud1529990"  // hier je password
               );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();
                OracleCommand cmd1 = conn.CreateCommand();
                cmd1.CommandText = "UPDATE PERSON SET SALDO = " + nwsaldo + " WHERE PERSON_ID = " + personid;
                cmd1.ExecuteNonQuery();
                b = true;
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("setSaldoPerson \n" + e.Message);
            }

            return b;
        }

        public bool setSaldoStore(int nwsaldos, int storeid)
        {
            bool b = false;

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
               "stud1529990", // hier je username, 
               "stud1529990"  // hier je password
               );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();
                OracleCommand cmd12 = conn.CreateCommand();
                cmd12.CommandText = "UPDATE PERSON SET SALDO = " + nwsaldos + " WHERE PERSON_ID = " + storeid;
                cmd12.ExecuteNonQuery();
                b = true;
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("setSaldoStore \n" + e.Message);
            }

            return b;
        }

        public Product getProduct(int productid)
        {
            Product p = new Product();;
            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();

                OracleCommand cmd3 = conn.CreateCommand();
                cmd3.CommandText = "select * from PRODUCT where PRODUCT_ID = " + productid;
                OracleDataReader myReader = cmd3.ExecuteReader();

                while (myReader.Read())
                {
                    p.ProductID = int.Parse(myReader["PRODUCT_ID"].ToString().Trim());
                    p.ProductName = myReader["PRODUCT_NAME"].ToString().Trim();
                    p.ProductPrice =  int.Parse(myReader["PRODUCT_PRICE"].ToString().Trim());
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("getamountperson error \n" + e.Message);
            }
            return p;
        }

        public List<Product> getProductListStore()
        {
            List<Product> listproduct = new List<Product>();
            
            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();

                OracleCommand cmd21 = conn.CreateCommand();
                cmd21.CommandText = "select * from JOIN_PRODUCTS_PERSON where PERSON_ID = 1";
                OracleDataReader myReader1 = cmd21.ExecuteReader();

                while (myReader1.Read())
                {
                    int a = 0;
                    Product p;
                    if(int.Parse(myReader1["AMOUNT"].ToString().Trim()) > 0)
                    {
                         a = int.Parse(myReader1["PRODUCT_ID"].ToString().Trim());
                         p = getProduct(a);
                         p.AvalibleProducts = int.Parse(myReader1["AMOUNT"].ToString().Trim());
                         listproduct.Add(p);
                    }
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("getamountperson error \n" + e.Message);
            }

            return listproduct;
        }

        public List<Product> getProductListPerson(int personid)
        {
            List<Product> listproduct = new List<Product>();

            String connectionString = String.Format(@"DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=ondora01.hu.nl)(PORT=8521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=cursus01.hu.nl)));PASSWORD={1};PERSIST SECURITY INFO=True;USER ID={0}",
                "stud1529990", // hier je username, 
                "stud1529990"  // hier je password
                );

            try
            {
                OracleConnection conn = new OracleConnection(connectionString);
                conn.Open();

                OracleCommand cmd21 = conn.CreateCommand();
                cmd21.CommandText = "select * from JOIN_PRODUCTS_PERSON where PERSON_ID = " + personid;
                OracleDataReader myReader1 = cmd21.ExecuteReader();

                while (myReader1.Read())
                {
                    int a = 0;
                    Product p;
                    a = int.Parse(myReader1["PRODUCT_ID"].ToString().Trim());
                    p = getProduct(a);
                    p.AvalibleProducts = int.Parse(myReader1["AMOUNT"].ToString().Trim());
                    listproduct.Add(p);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("getamountperson error \n" + e.Message);
            }

            return listproduct;
        }

        public string passwordgen(string s)
        {
            char[] stringarray = s.ToCharArray();
            Array.Reverse(stringarray);
            return new string(stringarray);

        }

    }
}
