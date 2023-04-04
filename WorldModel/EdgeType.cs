using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldModel
{
    public enum EdgeType
    {
        IsWithin,
        Contains,
        IsCarriedBy,
        Holds,
        IsIn,
        Hosts,
        IsSupporting,
        IsOn,
        LeadsTo
        }
}
