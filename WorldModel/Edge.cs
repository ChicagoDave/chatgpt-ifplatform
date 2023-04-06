using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldModel
{
    public class Edge
    {
        public string StartNodeId { get; private set; }
        public string EndNodeId { get; private set; }
        public List<GraphProperty> StartProperties { get; set; }
        public List<GraphProperty> EndProperties { get; set; }

        public Edge(string startNodeId, string endNodeId, GraphProperty startProperty, GraphProperty endProperty)
        {
            StartNodeId = startNodeId;
            EndNodeId = endNodeId;
            StartProperties = new List<GraphProperty>();
            StartProperties.Add(startProperty);
            EndProperties = new List<GraphProperty>();
            EndProperties.Add(endProperty);
        }
    }

}
