using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Databaseoef
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
            * Database bestaat uit Users table met Username en Password kolom.
            */
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection();

            //Vul hier je eigen connectie string in (rechtermuisknop op database connectie -> opties -> Connection String
            con.ConnectionString = @"Data Source=ruwan-flaptop\SQLEXPRESS;Initial Catalog=webstore;Integrated Security=True";


            con.Open();
            string query = "SELECT PersonName, PersonPassword FROM Person";
            
            SqlDataAdapter sdr = new SqlDataAdapter(query, con);
            sdr.Fill(ds);
            con.Close();
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    for (int k = 0; k < ds.Tables[i].Rows[j].ItemArray.Length; k++)
                    {
                        Console.Write("" + ds.Tables[i].Rows[j].ItemArray.GetValue(k).ToString());
                    }
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
            Console.WriteLine();

            con.Open();
            SqlDataReader myReader = null;
            SqlCommand myCommand = new SqlCommand("select * from Person", con);
            myReader = myCommand.ExecuteReader();
            while (myReader.Read())
            {
                Console.Write(myReader["PersonName"].ToString());
                Console.Write(myReader["PersonPassword"].ToString());
                Console.WriteLine();
            }

            con.Close();

            Console.ReadKey();

        }
    }
}
