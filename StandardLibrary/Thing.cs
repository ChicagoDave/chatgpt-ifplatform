using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary
{
    public class Thing : IThing
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string>? Synonyms { get; set; }
        public List<string>? Adjectives { get; set; }
        public Func<string>? Description { get; set; }
        public Func<bool>? Visible { get; set; }
        public Func<bool>? Edable { get; set; }
        public Func<bool>? Portable { get; set; }
        public Func<bool>? Wearable { get; set; }
        public Func<bool>? Lit { get; set; }
        public Func<bool>? Mentioned { get; set; }
        public Func<bool>? Seen { get; set; }
        public Func<bool>? Heard { get; set; }

        public Thing(string name, string description)
        {
            Id = IdGenerator.GetBase62(); // randonly generated 6 character unique id
            Name = name;
            Description = () => description;
       }
    }
}
