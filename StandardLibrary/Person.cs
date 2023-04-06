using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary
{
    public class Person : Thing, IPerson
    {
        public List<MemoryItem> Memory { get; set; }
        public HashSet<string> VisitedLocations { get; set; }
        public bool Alive { get; set; }

        public Person(string name, string description)
            : base(name, description)
        {
            Alive = true;
            Memory = new List<MemoryItem>();
            VisitedLocations = new HashSet<string>();
        }
    }
}
