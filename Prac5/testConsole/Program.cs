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
          ServiceStoreClient StoreProxy = new ServiceStoreClient();

          Console.WriteLine("test");
           StoreProxy.startup();
         //     Console.WriteLine(StoreProxy.startup());
        //      StoreProxy.setPerson("delta","atled");

            
               Console.WriteLine("test");
              if (StoreProxy.buyProduct(StoreProxy.getProductTestStore("map"), 1))
               {
                   Console.WriteLine("true");
               }
              else
               {
                   Console.WriteLine("error");
               }
             Console.ReadKey();
        }
    }
}
