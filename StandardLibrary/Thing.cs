using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary
{
    public class Thing : IThing
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Thing(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }

}
