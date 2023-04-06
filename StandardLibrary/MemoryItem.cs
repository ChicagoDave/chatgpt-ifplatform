using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary
{
    public class MemoryItem
    {
        public string LocationId { get; set; }
        public string Action { get; set; }
        public string Noun { get; set; }

        public MemoryItem(string locationId, string action, string noun)
        {
            LocationId = locationId;
            Action = action;
            Noun = noun;
        }
    }
}
