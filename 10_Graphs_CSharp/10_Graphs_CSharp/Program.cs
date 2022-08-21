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
        // need an ajacency list which is an array of linked lists


        public void AddNode(string label)
        {

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

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
    
}



