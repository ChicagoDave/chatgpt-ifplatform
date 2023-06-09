﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardLibrary
{

    public interface IThing
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
    }

    public interface IContainer : IThing
    {
        public Func<bool>? Closeable { get; set; }
        public Func<int>? MaximumWeight { get; set; }
        public Func<int>? MaximumSize { get; set; }
    }

    public interface IPerson : IThing
    {
        bool Alive { get; set; }
        List<MemoryItem> Memory { get; set; }
        HashSet<string> VisitedLocations { get; set; }
    }

    public interface IPlayer : IPerson
    {
    }
}
