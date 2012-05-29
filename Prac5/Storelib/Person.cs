using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Storelib
{
    class Person 
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public double Saldo { get; set; }
        public List<Product>PersonsProducts = new List<Product>();
        public Person(String name, String pw)
        {
            Name = name;
            Password = pw;
        }
    }
}
