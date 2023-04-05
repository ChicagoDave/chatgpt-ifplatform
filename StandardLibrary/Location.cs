using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StandardLibrary.Core;

namespace StandardLibrary
{
    public class Location : Thing
    {
        public Location(string name, string description)
            : base(name, description)
        {
        }
    }
}
