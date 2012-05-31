using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Storelib;
using System.ServiceModel;

namespace HostServer
{
    class Program
    {
       // private static ServiceStore serverstore = new ServiceStore();


        static void Main(string[] args)
        {
            Console.WriteLine("hsot consonle started");
            try
            {
                using (ServiceHost host = new ServiceHost(typeof(ServiceStore)))
                {
                    Console.WriteLine(" try to start host");
                    host.Open();
                    Console.WriteLine("host is running");
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
