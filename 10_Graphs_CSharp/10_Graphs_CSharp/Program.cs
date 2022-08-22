using System;
using System.Collections;


namespace Graphs
{

    class Graph
    {
        private class Node
        {
            private string _label;
            public Node(string label)
            {
                _label = label;
            }
            public override string ToString()
            {
                return _label;
            }
        }
        // need a hashmap for every label and it's index in the adjaceny list
        private Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
        // need an ajacency list which is an array of linked lists
        private Dictionary<Node, List<Node>> adjacencyList = new Dictionary<Node, List<Node>>();


        public void AddNode(string label)
        {
            var node = new Node(label);
            _nodes.Add(label, node);
            adjacencyList.Add(node, new List<Node>());
        }
        public void addEdge(string from, String to)
        {
            var fromNode = _nodes[from];
            if (fromNode == null)
                throw new Exception("Illegal From Arguement");
            var toNode = _nodes[to];
            if (toNode == null)
                throw new Exception("Illegal To Arguement");

            adjacencyList[fromNode].Add(toNode);
        }

        public void RemoveNode(string label)
        {

        }

        public void AddEdge()
        {

        }

        public void RemoveEdge()
        {

        }

        public void Print()
        {
            foreach (var source in adjacencyList.Keys)
            {
                var targets = adjacencyList[source];
                if (targets.Count > 0)
                {
                    Console.WriteLine($"{source} is connected to: ");
                    foreach (var target in targets)
                    {
                        Console.WriteLine($"{target}, ");
                    }
                }
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            var myGraph = new Graph();
            myGraph.AddNode("Andy");
            myGraph.AddNode("Barbara");
            myGraph.AddNode("Ed");
            myGraph.AddNode("Michael");
            myGraph.AddNode("Sofia");
            myGraph.AddNode("Juliana");
            myGraph.addEdge("Andy", "Ed");
            myGraph.addEdge("Barbara", "Ed");
            myGraph.addEdge("Andy", "Michael");
            myGraph.addEdge("Barbara", "Michael");
            myGraph.addEdge("Michael", "Juliana");
            myGraph.addEdge("Sofia", "Juliana");
            myGraph.Print();
        }
    }
    
}



