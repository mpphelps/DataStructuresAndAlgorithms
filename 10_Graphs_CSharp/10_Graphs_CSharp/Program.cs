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
        private Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        // need an ajacency list which is an array of linked lists
        private Dictionary<Node, List<Node>> adjacencyList = new Dictionary<Node, List<Node>>();


        public void AddNode(string label)
        {
            var node = new Node(label);
            nodes.Add(label, node);
            adjacencyList.Add(node, new List<Node>());
        }
        public void AddEdge(string from, String to)
        {
            var fromNode = nodes[from];
            if (fromNode == null)
                throw new Exception("Illegal From Arguement");
            var toNode = nodes[to];
            if (toNode == null)
                throw new Exception("Illegal To Arguement");

            adjacencyList[fromNode].Add(toNode);
        }

        public void RemoveNode(string label)
        {
            var node = nodes[label];
            if (node == null) return;
            foreach (var key in adjacencyList.Keys)
            {
                adjacencyList[key].Remove(node);
            }
            adjacencyList.Remove(node);
            nodes.Remove(node.ToString());
        }

        public void RemoveEdge(string from, string to)
        {
            var fromNode = nodes[from];
            var toNode = nodes[to];
            if (fromNode == null || toNode == null) return;
            adjacencyList[fromNode].Remove(toNode);
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
            myGraph.AddEdge("Andy", "Ed");
            myGraph.AddEdge("Barbara", "Ed");
            myGraph.AddEdge("Andy", "Michael");
            myGraph.AddEdge("Barbara", "Michael");
            myGraph.AddEdge("Michael", "Juliana");
            myGraph.AddEdge("Sofia", "Juliana");
            myGraph.Print();

        }
    }
    
}



