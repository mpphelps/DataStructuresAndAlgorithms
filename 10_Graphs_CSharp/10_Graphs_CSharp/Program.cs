using System.Threading.Channels;

namespace Graphs
{

    class Graph
    {
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
        public void TraverseDepthFirst(string root)
        {
            if (nodes[root] == null) return;
            TraverseDepthFirst(nodes[root], new HashSet<Node>());
        }
        private void TraverseDepthFirst(Node root, HashSet<Node> visited)
        {
            Console.WriteLine(root);
            visited.Add(root);

            foreach (var node in adjacencyList[root])
            {
                if (!visited.Contains(node))
                {
                    TraverseDepthFirst(node, visited);
                }
            }
        }
        public void TraverseDepthFirstIteratively(string root)
        {
            if (nodes[root] == null) return;
            Stack<Node> stack = new Stack<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            stack.Push(nodes[root]);
            while (stack.Count > 0)
            {
                Node current = stack.Pop();
                if (visited.Contains(current))
                    continue;
                Console.WriteLine(current);
                visited.Add(current);

                foreach (var neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                    }
                }
            }
        }
        public void TraverseBreadthFirst(string root)
        {
            if (nodes[root] == null) return;
            TraverseBreadthFirst(nodes[root], new Queue<Node>());
        }
        private void TraverseBreadthFirst(Node root, Queue<Node> visited)
        {
            Console.WriteLine(root);

            foreach (var node in adjacencyList[root])
            {
                if (!visited.Contains(node))
                {
                    visited.Enqueue(node);
                }
            }

            if (visited.Count > 0)
            {
                Node next = visited.Dequeue();
                TraverseBreadthFirst(next, visited);
            }
        }
        public List<string> TopologicalSort()
        {
            Stack<Node> stack = new Stack<Node>();
            HashSet<Node> visited = new HashSet<Node>();
            foreach (var node in nodes.Values)
            {
                TopologicalSort(node, visited, stack);
            }

            List<string> sorted = new List<string>();
            while (stack.Count > 0)
            {
                sorted.Add(stack.Pop().ToString());
            }

            return sorted;

        }
        private void TopologicalSort(Node node, HashSet<Node> visited, Stack<Node> stack)
        {
            if (visited.Contains(node)) return;

            visited.Add(node);
            foreach (var neighbor in adjacencyList[node])
            {
                TopologicalSort(neighbor,visited,stack);
            }

            stack.Push(node);
        }
        public bool HasCycle()
        {
            HashSet<Node> allNodes = new HashSet<Node>();
            HashSet<Node> visiting = new HashSet<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            foreach (var node in nodes.Values)
            {
                allNodes.Add(node);
            }

            int i = 0;
            while (i < allNodes.Count)
            {
                var current = allNodes.ToArray()[i];
                if(HasCycle(current, allNodes, visiting, visited))
                    return true;
                i++;
            }

            return false;
        }
        private bool HasCycle(Node node, HashSet<Node> allNodes, HashSet<Node> visiting, HashSet<Node> visited)
        {
            allNodes.Remove(node);
            visiting.Add(node);
            foreach (var neighbor in adjacencyList[node])
            {
                if (visited.Contains(neighbor))
                {
                    continue;
                }

                if (visiting.Contains(neighbor))
                {
                    return true;
                }

                if (HasCycle(neighbor, allNodes, visiting, visited))
                    return true;

            }

            visiting.Remove(node);
            visited.Add(node);
            return false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var myGraph = new Graph();
            //myGraph.AddNode("Andy");
            //myGraph.AddNode("Barbara");
            //myGraph.AddNode("Ed");
            //myGraph.AddNode("Michael");
            //myGraph.AddNode("Sofia");
            //myGraph.AddNode("Juliana");
            //myGraph.AddEdge("Andy", "Ed");
            //myGraph.AddEdge("Barbara", "Ed");
            //myGraph.AddEdge("Andy", "Michael");
            //myGraph.AddEdge("Barbara", "Michael");
            //myGraph.AddEdge("Michael", "Juliana");
            //myGraph.AddEdge("Sofia", "Juliana");
            //myGraph.Print();

            //myGraph.AddNode("A");
            //myGraph.AddNode("B");
            //myGraph.AddNode("C");
            //myGraph.AddNode("D");
            //myGraph.AddNode("E");
            //myGraph.AddEdge("C", "A");
            //myGraph.AddEdge("C", "B");
            //myGraph.AddEdge("C", "D");
            //myGraph.AddEdge("A", "B");
            //myGraph.AddEdge("A", "E");
            //myGraph.AddEdge("B", "E");
            //myGraph.TraverseDepthFirst("A");
            //myGraph.TraverseDepthFirstIteratively("A");
            //myGraph.TraverseBreadthFirst("C");

            //myGraph.AddNode("X");
            //myGraph.AddNode("A");
            //myGraph.AddNode("B");
            //myGraph.AddNode("P");
            //myGraph.AddEdge("X", "A");
            //myGraph.AddEdge("X", "B");
            //myGraph.AddEdge("A", "P");
            //myGraph.AddEdge("B", "P");
            //var list = myGraph.TopologicalSort();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}

            myGraph.AddNode("A");
            myGraph.AddNode("B");
            myGraph.AddNode("C");
            myGraph.AddNode("D");
            myGraph.AddEdge("D", "A");
            myGraph.AddEdge("A", "B");
            myGraph.AddEdge("B", "C");
            myGraph.AddEdge("C", "A");
            Console.WriteLine(myGraph.HasCycle());

        }
    }
    
}



