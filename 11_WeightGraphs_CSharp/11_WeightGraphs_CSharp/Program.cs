using System;

namespace WeightGraphs
{
    class WeightedGraph
    {
        private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
        private Dictionary<Node, List<Edge>> _adjacencyList = new Dictionary<Node, List<Edge>>();
        private class Node
        {
            private readonly string _label;
            public Node(string label)
            {
                _label = label;
            }
            public override string ToString()
            {
                return _label;
            }
        }
        private class Edge
        {
            private Node _from;
            private Node _to;
            private int _weight;

            public Edge(Node from, Node to, int weight)
            {
                _from = from;
                _to = to;
                _weight = weight;
            }
            public override string ToString()
            {
                return $"{_from} --{_weight}--> {_to}";
            }
        }
        public void AddNode(string label)
        {
            var node = new Node(label);
            _nodes.Add(label, node);
            _adjacencyList.Add(node, new List<Edge>());
        }
        public void AddEdge(string from, string to, int weight)
        {
            var fromNode = _nodes[from];
            if (fromNode == null)
                throw new Exception("Illegal From Arguement");
            var toNode = _nodes[to];
            if (toNode == null)
                throw new Exception("Illegal To Arguement");
            _adjacencyList[fromNode].Add(new Edge(fromNode, toNode, weight));
            _adjacencyList[toNode].Add(new Edge(toNode, fromNode, weight));
        }
        public void Print()
        {
            foreach (var node in _adjacencyList)
            {
                foreach (var edge in node.Value)
                {
                    Console.WriteLine(edge.ToString());
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var myGraph = new WeightedGraph();
            myGraph.AddNode("A");
            myGraph.AddNode("B");
            myGraph.AddNode("C");
            myGraph.AddEdge("A", "B", 3);
            myGraph.AddEdge("A", "C", 2);
            myGraph.Print();
        }
    }
}

