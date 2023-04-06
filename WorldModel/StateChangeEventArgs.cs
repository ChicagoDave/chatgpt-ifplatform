using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldModel
{
    public class StateChangeEventArgs : EventArgs
    {
        public StateChangeEventType EventType { get; }
        public Node? ChangedNode { get; }
        public Edge? ChangedEdge { get; }

        public StateChangeEventArgs(StateChangeEventType eventType, Node? changedNode = null, Edge? changedEdge = null)
        {
            EventType = eventType;
            ChangedNode = changedNode;
            ChangedEdge = changedEdge;
        }
    }

}
