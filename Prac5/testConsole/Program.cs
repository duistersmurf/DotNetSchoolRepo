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

          Console.WriteLine("test console is running");
           StoreProxy.startup();
         //     Console.WriteLine(StoreProxy.startup());
        //      StoreProxy.setPerson("delta","atled");

            
               Console.WriteLine("startup completed");
              /*if (StoreProxy.login("Henk","kneH"))
               {
                   Console.WriteLine("true");
               }
              else
               {
                   Console.WriteLine("error");
               }*/
              if (StoreProxy.buyProduct(StoreProxy.getProductTestStore("map"), 1))
              {
                  Console.WriteLine("true");
              }
              else
              {
                  Console.WriteLine("false");
              }
            /* if (StoreProxy.signup("Piet", "tieP"))
              {
                  Console.WriteLine("true");
              }
              else
              {
                  Console.WriteLine("false");
              }*/
             Console.ReadKey();
        }
    }
}
