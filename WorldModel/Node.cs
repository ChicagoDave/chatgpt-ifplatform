using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldModel
{
    public class Node
    {
        public string Id { get; private set; }
        public object Data { get; private set; }
        public List<Edge> Edges { get; private set; }
        public List<GraphProperty> Properties { get; set; }

        public Node(string id, object data, GraphProperty defaultProperty)
        {
            Id = id;
            Data = data;
            Edges = new List<Edge>();
            Properties = new List<GraphProperty>();
            Properties.Add(defaultProperty);
        }

        public Node(string id, object data, List<GraphProperty> defaultProperties)
        {
            Id = id;
            Data = data;
            Edges = new List<Edge>();
            Properties = new List<GraphProperty>();
            Properties.AddRange(defaultProperties);
        }
    }
}
