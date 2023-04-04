using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StandardLibrary.Core;

namespace StandardLibrary
{
    public class Location : Thing, ILocation
    {
        public Location(string id, string name, string description)
            : base(id, name, description)
        {
        }
    }
}
