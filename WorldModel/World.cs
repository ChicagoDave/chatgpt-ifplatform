namespace WorldModel
{
    public class World
    {
        public Dictionary<string, Node> Nodes { get; private set; }

        public World()
        {
            Nodes = new Dictionary<string, Node>();
        }

        public void AddNode(string id, object data)
        {
            Nodes[id] = new Node(id, data);
        }

        public void ConnectNodes(string id1, string id2, EdgeType edgeType, string label)
        {
            Edge edge1 = new Edge(id1, id2, edgeType);
            edge1.Properties["Label"] = label; // Set the Label property
            Nodes[id1].Edges.Add(edge1);

            if (edgeType == EdgeType.LeadsTo)
            {
                Edge edge2 = new Edge(id2, id1, edgeType);
                edge2.Properties["Label"] = label; // Set the Label property
                Nodes[id2].Edges.Add(edge2);
            }
        }

        private EdgeType GetReverseEdgeType(EdgeType edgeType)
        {
            switch (edgeType)
            {
                case EdgeType.IsWithin:
                    return EdgeType.Contains;
                case EdgeType.Contains:
                    return EdgeType.IsWithin;
                case EdgeType.IsCarriedBy:
                    return EdgeType.Holds;
                case EdgeType.Holds:
                    return EdgeType.IsCarriedBy;
                case EdgeType.IsIn:
                    return EdgeType.Hosts;
                case EdgeType.Hosts:
                    return EdgeType.IsIn;
                case EdgeType.IsSupporting:
                    return EdgeType.IsOn;
                case EdgeType.IsOn:
                    return EdgeType.IsSupporting;
                case EdgeType.LeadsTo:
                    return EdgeType.LeadsTo;
                default:
                    throw new ArgumentException("Invalid edge type provided.");
            }
        }
    }
}
