using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace prac4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test");
            List<CD> cds = CreateCDList();

            var cdXML = new XDocument();
            var rootElem = new XElement("cd");
            cdXML.Add(rootElem);
            foreach (CD cd in cds)
            {
                var cdElem = new XElement("cd");
                var Title = new XElement("Title", cd.Title);
                    cdElem.Add(Title);
                var Artiest = new XElement("Artiest", cd.Artiest);
                    cdElem.Add(Artiest);
             var tracksElem = new XElement("Tracks");
           
               foreach (Track track in cd.tracks)
                {
                    var trackElem = new XElement("track");
                    var titleElem = new XElement("Title", track.Title);
                     trackElem.Add(titleElem);
                    var artietsElem = new XElement("Artiets", track.Artiest);
                    trackElem.Add(artietsElem);
                     var lenghtElem = new XElement("Lenght", track.lenght);
                     trackElem.Add(lenghtElem);
                     tracksElem.Add(trackElem);
                }
               cdElem.Add(tracksElem);
                rootElem.Add(cdElem);
            }
            Console.WriteLine(cdXML.ToString());
            Console.Read();
        }


        private static List<CD> CreateCDList()
        {
            Console.WriteLine("test2");
            List<CD> cds = new List<CD>();
            CD cd1 = new CD("RainBow", "DIO");                           
                Track tr1 = new Track("rainbow", "dio", TimeSpan.Parse("0:06:00"));
                Track tr2 = new Track("flight", "dio", TimeSpan.Parse("0:06:00"));
                List<Track> tlist1 = new List<Track>();
                tlist1.Add(tr1);
                tlist1.Add(tr2);
               cd1.tracks = tlist1;
                 cds.Add(cd1);

            return cds;
        }
    }
    class Track
    {
        public Track(string tle, string art, TimeSpan lgt)
        {
            Title = tle;
            Artiest = art;
            lenght = lgt;
        }
        public String Title{ get; set;}
        public String Artiest{get; set;}
        public TimeSpan lenght{get; set;}
    }
    class CD
    {
        public CD(string tle, string art) 
        {
            Title = tle;
            Artiest = art;
        }
        public String Title{ get; set;}
        public String Artiest { get; set;}
        public List<Track> tracks { get; set; }

        public String tostring()
        {
            string s = "kaas" + Title + " " + Artiest + " ";
            foreach (Track t in tracks)
            {
                s += " " + t;
            }
            return s;
        }
        public void kaas()
        {
            foreach (Track t in tracks)
            {
                Console.WriteLine(t);
            }
        }

    }

    

}
