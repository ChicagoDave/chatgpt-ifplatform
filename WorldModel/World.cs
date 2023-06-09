﻿namespace WorldModel
{
    public class World
    {
        public Dictionary<string, Node> Nodes { get; private set; }

        public event EventHandler<StateChangeEventArgs>? StateChanged;

        public World()
        {
            Nodes = new Dictionary<string, Node>();
        }

        public void AddNode(string id, object data, GraphProperty defaultProperty)
        {
            Nodes[id] = new Node(id, data, defaultProperty);
            NodeAdded(Nodes[id]);
        }

        public void AddNode(string id, object data, List<GraphProperty> defaultProperties)
        {
            Nodes[id] = new Node(id, data, defaultProperties);
            NodeAdded(Nodes[id]);
        }

        public void ConnectNodes(string startNodeId, string endNodeId, GraphProperty startProperty, GraphProperty endProperty)
        {
            // Check if an edge between startNodeId and endNodeId already exists
            Edge? existingEdge = FindExistingEdge(startNodeId, endNodeId);

            if (existingEdge != null)
            {
                throw new InvalidOperationException("An edge between the specified nodes already exists. To replace an edge, use the ReplaceEdge method.");
            }
            else
            {
                // Create a new edge and add it to both nodes
                Edge bidirectionalEdge = new Edge(startNodeId, endNodeId, startProperty, endProperty);
                Nodes[startNodeId].Edges.Add(bidirectionalEdge);
                Nodes[endNodeId].Edges.Add(bidirectionalEdge);
                EdgeAdded(bidirectionalEdge);
            }
        }

        public void ReplaceOrAddEdge(string startNodeId, string endNodeId, GraphProperty startProperty, GraphProperty endProperty)
        {
            // Find existing edge and remove it
            Edge? existingEdge = FindExistingEdge(startNodeId, endNodeId);

            if (existingEdge != null)
            {
                // Remove the existing edge from both nodes
                Nodes[startNodeId].Edges.Remove(existingEdge);
                Nodes[endNodeId].Edges.Remove(existingEdge);
            }

            // Create a new edge and add it to both nodes
            Edge bidirectionalEdge = new Edge(startNodeId, endNodeId, startProperty, endProperty);
            Nodes[startNodeId].Edges.Add(bidirectionalEdge);
            Nodes[endNodeId].Edges.Add(bidirectionalEdge);
            EdgeAdded(bidirectionalEdge);
        }

        private Edge? FindExistingEdge(string startNodeId, string endNodeId)
        {
            return Nodes[startNodeId].Edges.FirstOrDefault(edge => edge.EndNodeId == endNodeId);
        }

        public void RemoveNode(string id)
        {
            if (Nodes.ContainsKey(id))
            {
                Node nodeToRemove = Nodes[id];
                List<Edge> edgesToRemove = new List<Edge>(nodeToRemove.Edges);

                // Remove all edges connected to this node
                foreach (var edge in edgesToRemove)
                {
                    RemoveEdge(edge.StartNodeId, edge.EndNodeId);
                }

                Nodes.Remove(id);
                NodeRemoved(nodeToRemove);
            }
            else
            {
                throw new ArgumentException($"Node with id '{id}' not found.");
            }
        }

        public void RemoveEdge(string sourceId, string targetId)
        {
            var sourceNode = Nodes[sourceId];
            var targetNode = Nodes[targetId];

            var edgeToRemove = sourceNode.Edges.FirstOrDefault(e => e.EndNodeId == targetId);
            var reverseEdgeToRemove = targetNode.Edges.FirstOrDefault(e => e.EndNodeId == sourceId);

            if (edgeToRemove != null && reverseEdgeToRemove != null)
            {
                sourceNode.Edges.Remove(edgeToRemove);
                targetNode.Edges.Remove(reverseEdgeToRemove);
                EdgeRemoved(edgeToRemove);
            }
            else
            {
                throw new ArgumentException($"Edge between '{sourceId}' and '{targetId}' not found.");
            }
        }

        public void MoveEdge(string sourceId, string oldTargetId, string newTargetId, GraphProperty startProperty, GraphProperty endProperty)
        {
            if (Nodes.ContainsKey(newTargetId))
            {
                Edge? edge = Nodes[sourceId].Edges.FirstOrDefault(e => e.EndNodeId == oldTargetId);
                if (edge != null)
                {
                    RemoveEdge(sourceId, oldTargetId);
                    ConnectNodes(sourceId, newTargetId, startProperty, endProperty);
                }
            }
            else
            {
                throw new ArgumentException($"Target node with id '{newTargetId}' not found.");
            }
        }

        protected virtual void OnStateChanged(StateChangeEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }

        public void NodeAdded(Node node)
        {
            OnStateChanged(new StateChangeEventArgs(StateChangeEventType.NodeAdded, node));
        }

        public void NodeRemoved(Node node)
        {
            OnStateChanged(new StateChangeEventArgs(StateChangeEventType.NodeRemoved, node));
        }

        public void EdgeAdded(Edge edge)
        {
            OnStateChanged(new StateChangeEventArgs(StateChangeEventType.EdgeAdded, null, edge));
        }

        public void EdgeRemoved(Edge edge)
        {
            OnStateChanged(new StateChangeEventArgs(StateChangeEventType.EdgeRemoved, null, edge));
        }
    }
}
