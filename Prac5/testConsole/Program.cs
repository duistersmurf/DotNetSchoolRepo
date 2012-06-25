using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using testConsole.StoreService;


namespace testConsole
{
    class Program
    {        
        static void Main(string[] args)
        {
           Person p = new Person();
           List<Product> lps = new List<Product>();
           List<Product> lpp = new List<Product>();
        ServiceStoreClient StoreProxy = new ServiceStoreClient();

          Console.WriteLine("test console is running");
          Console.WriteLine(StoreProxy.startup());                     
          Console.WriteLine("startup completed");
          Console.WriteLine(StoreProxy.signup("Henk"));
          Console.WriteLine("start login");
              p = (Person)StoreProxy.login("Henk", "Henk123");
           Console.WriteLine(p.Name);
               if (StoreProxy.buyProduct(StoreProxy.getProduct(1), 2, p))
               {
                   Console.WriteLine("nice");
               }
               else
               {
                   Console.WriteLine("error");
               }       

             Console.ReadKey();
        }

    }
}
