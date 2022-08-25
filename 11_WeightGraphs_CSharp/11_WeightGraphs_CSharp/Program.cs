using System;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace WeightGraphs
{
    public class WeightedGraph
    {
        private readonly Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
        //private readonly Dictionary<Node, List<Edge>> _adjacencyList = new Dictionary<Node, List<Edge>>();
        private class Node
        {
            private readonly string _label;
            private readonly List<Edge> _edges = new List<Edge>();
            public Node(string label)
            {
                _label = label;
            }
            public override string ToString()
            {
                return _label;
            }
            public string GetLabel()
            {
                return _label;
            }
            public void AddEdge(Node to, int weight)
            {
                _edges.Add(new Edge(this, to, weight));
            }
            public List<Edge> GetEdges()
            {
                return _edges;
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
                return $"{_from} <--{_weight}--> {_to}";
            }
            public Node GetFrom()
            {
                return _from;
            }
            public Node GetTo()
            {
                return _to;
            }
            public int GetWeight()
            {
                return _weight;
            }
        }
        private Node GetNode(string value)
        {
            if (!IsValidNode(value)) throw new Exception("Illegal Argument");
            return _nodes[value];
        }
        private bool IsValidNode(string value)
        {
            return _nodes.ContainsKey(value);
        }
        private static Path BuildPath(Dictionary<Node, Node> previousNodes, Node toNode)
        {
            Stack<Node> stack = new Stack<Node>();
            var path = new Path();
            stack.Push(toNode);
            var previous = previousNodes[toNode];
            while (previous != null)
            {
                stack.Push(previous);
                previous = previousNodes[previous];
            }
            while (stack.Count > 0)
            {
                path.Add(stack.Pop().ToString());
            }

            return path;
        }
        private static bool HasCycle(Node node, Node parent, HashSet<Node> visited)
        {
            visited.Add(node);
            {
                foreach (var edge in node.GetEdges())
                {
                    if (edge.GetTo() == parent)
                        continue;
                    if (visited.Contains(edge.GetTo()) ||
                        HasCycle(edge.GetTo(), node, visited))
                        return true;
                }
            }
            return false;
        }
        public bool ContainsNode(string value)
        {
            return _nodes.ContainsKey(value);
        }
        public void AddNode(string label)
        {
            _nodes.Add(label, new Node(label));
        }
        public void AddEdge(string from, string to, int weight)
        {
            var fromNode = GetNode(from);
            var toNode = GetNode(to);
            fromNode.AddEdge(toNode,weight); 
            toNode.AddEdge(fromNode, weight);
        }
        public void Print()
        {
            var visited = new HashSet<Node>();
            foreach (var node in _nodes.Values)
            {
                visited.Add(node);
                foreach (var edge in node.GetEdges())
                {
                    if(visited.Contains(edge.GetTo()))
                        continue;
                    Console.WriteLine(edge.ToString());
                }
            }
        }
        public (Path, int) GetShortestPathAndDistance(string from, string to)
        {
            // This is implement used Dijkstra's Algorithm
            // Breadth first search
            // Implemented using a priority queue

            // For each neighbor
            //   if found shorter path
            //      update distances table
            //      push neighbor to queue
            var fromNode = GetNode(from);
            var toNode = GetNode(to);

            var queue = new PriorityQueue<Node, int>();
            var distances = new Dictionary<Node, int>();
            var previousNodes = new Dictionary<Node, Node>();
            var visited = new HashSet<Node>();
            // Initialize distances table
            foreach (var node in _nodes.Values)
            {
                distances.Add(node, Int32.MaxValue);
                previousNodes.Add(node, null);
            }
            distances[fromNode] = 0;
            queue.Enqueue(fromNode,0);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                visited.Add(currentNode);

                foreach (var edge in currentNode.GetEdges())
                {
                    if (visited.Contains(edge.GetTo())) 
                        continue;
                    var newDistance = distances[currentNode] + edge.GetWeight();
                    if (newDistance < distances[edge.GetTo()])
                    {
                        distances[edge.GetTo()] = newDistance;
                        previousNodes[edge.GetTo()] = currentNode;
                        queue.Enqueue(edge.GetTo(), newDistance);
                    }
                }
            }
            return (BuildPath(previousNodes, toNode), distances[toNode]);
        }
        public bool HasCycle()
        {
            HashSet<Node> visited = new HashSet<Node>();

            foreach (var node in _nodes.Values)
            {
                if (!visited.Contains(node) &&
                    HasCycle(node, null, visited))
                    return true;
            }

            return false;
        }
        public WeightedGraph GetMinimumSpanningTree()
        {
            var tree = new WeightedGraph();
            var edges = new PriorityQueue<Edge, int>();
            var startNode = _nodes.Values.GetEnumerator();
            startNode.MoveNext();
            foreach (var edge in startNode.Current.GetEdges())
                edges.Enqueue(edge, edge.GetWeight());
            tree.AddNode(startNode.Current.GetLabel());
            if (edges.Count == 0) return tree;
            while (tree._nodes.Count < _nodes.Count)
            {
                var minEdge = edges.Dequeue();
                var nextNode = minEdge.GetTo();
                if (tree.ContainsNode(nextNode.GetLabel()))
                    continue;
                tree.AddNode(nextNode.GetLabel());
                tree.AddEdge(minEdge.GetFrom().GetLabel(), nextNode.GetLabel(), minEdge.GetWeight());
                foreach (var edge in nextNode.GetEdges())
                    if (!tree.ContainsNode(edge.GetTo().GetLabel()))
                        edges.Enqueue(edge, edge.GetWeight());
            }
            return tree;
        }
    }
    public class Path
    {
        private readonly List<string> _nodes = new List<string>();
        public void Add(string node)
        {
            _nodes.Add(node);
        }
        public override string ToString()
        {
            var nodeString = "";
            foreach (var node in _nodes)
            {
                nodeString += node.ToString() + ", ";
            }
            return nodeString;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region Test Graph Add and Print
            Console.WriteLine("\nTest Graph Add and Print");
            var myGraph = new WeightedGraph();
            myGraph.AddNode("A");
            myGraph.AddNode("B");
            myGraph.AddNode("C");
            myGraph.AddEdge("A", "B", 3);
            myGraph.AddEdge("A", "C", 2);
            myGraph.Print();

            PriorityQueue<string, int> _queue = new PriorityQueue<string, int>() { };
            _queue.Enqueue("Michael", 10);
            _queue.Enqueue("Sofia", 15);
            _queue.Enqueue("Juliana", 25);
            _queue.Enqueue("Lisa", 5);
            _queue.Enqueue("Chris", 10);
            var count = _queue.Count;
            for (var i = 0; i < count; i++)
            {
                Console.WriteLine(_queue.Dequeue());
            }
            #endregion
            #region Test1 Graph Shortest Path Algorithm
            Console.WriteLine("\nTest1 Graph Shortest Path Algorithm");
            var myGraph1 = new WeightedGraph();
            myGraph1.AddNode("A");
            myGraph1.AddNode("B");
            myGraph1.AddNode("C");
            myGraph1.AddNode("D");
            myGraph1.AddNode("E");
            myGraph1.AddEdge("A", "B", 3);
            myGraph1.AddEdge("A", "C", 4);
            myGraph1.AddEdge("A", "D", 2);
            myGraph1.AddEdge("B", "D", 6);
            myGraph1.AddEdge("B", "E", 1);
            myGraph1.AddEdge("C", "D", 1);
            myGraph1.AddEdge("C", "D", 5);
            var shortestPath1 = myGraph1.GetShortestPathAndDistance("A", "E");
            Console.WriteLine("Shorted path from A to E: ");
            Console.WriteLine(shortestPath1.Item1);
            Console.WriteLine("Shorted distance from A to E: ");
            Console.WriteLine(shortestPath1.Item2);
            #endregion
            #region Test2 Graph Shortest Path Algorithm
            Console.WriteLine("\nTest2 Graph Shortest Path Algorithm");
            var myGraph2 = new WeightedGraph();
            myGraph2.AddNode("A");
            myGraph2.AddNode("B");
            myGraph2.AddNode("C");
            myGraph2.AddEdge("A", "B", 1);
            myGraph2.AddEdge("B", "C", 2);
            myGraph2.AddEdge("A", "C", 10);
            var shortestPath2 = myGraph2.GetShortestPathAndDistance("A", "C");
            Console.WriteLine("Shorted path from A to C: ");
            Console.WriteLine(shortestPath2.Item1);
            Console.WriteLine("Shorted distance from A to C: ");
            Console.WriteLine(shortestPath2.Item2);
            #endregion
            #region Test Graph Has Cycle Algorithm
            Console.WriteLine("\nTest Graph Has Cycle Algorithm");
            var myGraph3 = new WeightedGraph();
            myGraph3.AddNode("A");
            myGraph3.AddNode("B");
            myGraph3.AddNode("C");
            myGraph3.AddEdge("A", "B", 1);
            myGraph3.AddEdge("B", "C", 2);
            myGraph3.AddEdge("C", "A", 10);
            myGraph3.Print();
            Console.WriteLine($"Graph has cycle? {myGraph3.HasCycle()}");
            #endregion
            #region Test Graph Min Span Tree
            Console.WriteLine("\nTest Graph Has Cycle Algorithm");
            var myGraph4 = new WeightedGraph();
            myGraph4.AddNode("A");
            myGraph4.AddNode("B");
            myGraph4.AddNode("C");
            myGraph4.AddNode("D");
            myGraph4.AddEdge("A", "B", 3);
            myGraph4.AddEdge("B", "D", 4);
            myGraph4.AddEdge("C", "D", 5);
            myGraph4.AddEdge("A", "C", 1);
            myGraph4.AddEdge("B", "C", 2);
            Console.WriteLine($"Graph:");
            myGraph4.Print();
            Console.WriteLine($"Graph's minimum spanning tree:");
            var myGraph4SpanningTree = myGraph4.GetMinimumSpanningTree();
            myGraph4SpanningTree.Print();
            #endregion
        }
    }
}

