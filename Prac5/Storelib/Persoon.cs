using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Storelib
{
    [DataContract]
    public class Persoon
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public Persoon(String name)
        {
            Name = name;
        }
    }
}
