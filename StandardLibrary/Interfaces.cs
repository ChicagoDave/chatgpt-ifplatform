using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary
{
    public interface ILocation
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
    }

    public interface IThing
    {
        string Id { get; }
        string Name { get; }
        string Description { get; }
    }

    public interface IPerson : IThing
    {
        bool Alive { get; set; }
    }

    public interface IPlayer : IPerson
    {
    }
}
