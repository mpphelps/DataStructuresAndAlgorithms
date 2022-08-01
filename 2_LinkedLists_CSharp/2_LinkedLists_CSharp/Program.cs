using System;

namespace LinkedLists
{

    class LinkedList<T> where T : IComparable
    {
    /*  Linked List Structure
        *  NODE -> NODE -> NODE -> NODE
        *   ^                       ^
        *  TAIL                    HEAD
    */
        private class Node<NodeType>
        {
            public NodeType? NodeValue { get; set; }
            public Node<NodeType>? NextNode { get; set; }
            public Node()
            {
                NodeValue = default(NodeType);
                NextNode = null;
            }
            public Node(NodeType value)
            {
                NodeValue = value;
                NextNode = null;
            }
            public Node(NodeType value, Node<NodeType> next)
            {
                NodeValue = value;
                NextNode = next;
            }
            ~Node()
            {

            }
        }
        private Node<T> _head;
        private Node<T> _tail;
        private int _size;
        public LinkedList()
        {
            _head = new Node<T>();
            _tail = new Node<T>();
        }
        private bool isEmpty()
        {
            return ((_tail.NextNode == null) && (_head.NextNode == null)) ? true : false;
        }
        public int GetSize()
        {
            return _size;
        }
        public void AddFirst(T value)
        {
            Node<T> node = new Node<T>(value);
            if (isEmpty())
            {
                _tail.NextNode = node;
                _head.NextNode = node;
            }
            else
            {
                node.NextNode = _tail.NextNode;
                _tail.NextNode = node;
            }
            _size++;
        }
        public void AddLast(T value)
        {
            Node<T> node = new Node<T>(value);
            if (isEmpty())
            {
                _tail.NextNode = node;
                _head.NextNode = node;
            }
            else
            {
                _head.NextNode.NextNode = node;
                _head.NextNode = node;
            }
            _size++;
        }
        public void DeleteFirst()
        {
            Node<T>? tempNode = new Node<T>();
            tempNode.NextNode = _tail.NextNode;
            if (isEmpty())
            {
                return;
            }
            else if (GetSize() == 1)
            {
                _tail.NextNode = null;
                _head.NextNode = null;
                tempNode = null;
            }
            else
            {
                _tail.NextNode = _tail.NextNode.NextNode;
            }
            _size--;
            Console.WriteLine("Deleted first node from list");
        }
        public void DeleteLast()
        {
            Node<T>? tempNode = new Node<T>();
            tempNode.NextNode = _tail.NextNode;
            if (isEmpty())
            {
                return;
            }
            else if (GetSize() == 1)
            {
                _tail.NextNode = null;
                _head.NextNode = null;
                tempNode = null;
            }
            else
            {
                while (tempNode.NextNode.NextNode != null)
                {
                    tempNode = tempNode.NextNode;
                    if (tempNode.NextNode.NextNode == null)
                    {
                        _head.NextNode = tempNode;
                        tempNode.NextNode = null;
                        break;
                    }
                }
            }
            _size--;
            Console.WriteLine("Deleted last node from list");
        }
        public bool Contains(T value)
        {
            if (_tail.NextNode != null)
            {
                Node<T>? nodePtr = _tail.NextNode;
                while (true)
                {
                    if (value.Equals(nodePtr.NodeValue)) return true;
                    else if (nodePtr.NextNode == null) return false;
                    else nodePtr = nodePtr.NextNode;
                }
            }
            return false;
        }
        public int IndexOf(T value)
        {
            int index = 0;
            if (_tail.NextNode != null)
            {
                Node<T>? nodePtr = _tail.NextNode;
                while (true)
                {
                    if (value.Equals(nodePtr.NodeValue)) return index;
                    else if (nodePtr.NextNode == null) return index;
                    else
                    {
                        nodePtr = nodePtr.NextNode;
                        index++;
                    }
                }
            }
            return -1;
        }
        public void Print()
        {
            Node<T> nodePtr = _tail.NextNode;
            Console.Write("LinkedList = { ");
            if (nodePtr != null)
            {
                while (nodePtr.NextNode != null)
                {
                    Console.Write($"{nodePtr.NodeValue}, ");
                    nodePtr = nodePtr.NextNode;
                }
                Console.Write(nodePtr.NodeValue);
            }
            Console.WriteLine(" }");
        }
        public T[] ToArray()
        {
            T[] tempArray = new T[_size];
            Node<T>? tempNode = new Node<T>();
            int index = 0;
            tempNode = _tail;
            while (tempNode.NextNode != null)
            {
                tempNode = tempNode.NextNode;
                tempArray[index] = tempNode.NodeValue;
                index++;
            }
            return tempArray;
        }
        public void Reverse()
        {
            if (_size == 0 || _size == 1) { }
            else if (_size == 2)
            {
                Node<T>? tempNode = new Node<T>();
                _head.NextNode.NextNode = _tail.NextNode;
                _tail.NextNode.NextNode = null;
                tempNode.NextNode = _head.NextNode;
                _head.NextNode = _tail.NextNode;
                _tail.NextNode = tempNode.NextNode;
                tempNode = null;
            }
            else
            {
                // Allocate three temps nodes on the heap
                Node<T>? previousNode = new Node<T>();
                Node<T>? currentNode = new Node<T>();
                Node<T>? nextNode = new Node<T>();

                // Set the temp nodes to first three nodes in list
                previousNode.NextNode = _tail.NextNode;
                currentNode.NextNode = _tail.NextNode.NextNode;
                nextNode.NextNode = _tail.NextNode.NextNode.NextNode;

                // Change the first nodes nextnode to null and move the head;
                previousNode.NextNode.NextNode = null;
                _head.NextNode = _tail.NextNode;

                while (nextNode.NextNode != null)
                {
                    // Change node2 next node to previous node
                    currentNode.NextNode.NextNode = previousNode.NextNode;
                    previousNode.NextNode = currentNode.NextNode;
                    currentNode.NextNode = nextNode.NextNode;
                    nextNode.NextNode = nextNode.NextNode.NextNode;
                }
                // Change the last nodes nextNode and move the tail
                currentNode.NextNode.NextNode = previousNode.NextNode;
                _tail.NextNode = currentNode.NextNode;

                // Delete temp nodes from heap
                previousNode = null;
                currentNode = null;
                nextNode = null;
            }
        }
        public T GetKthNodeFromEnd(int index)
        {
            if (index > _size || index < 0)
                throw new InvalidOperationException("Invalid index");
            index = _size - index;
            Node<T>? KthNode = new Node<T>();
            KthNode.NextNode = _tail.NextNode;
            while (index > 0)
            {
                KthNode = KthNode.NextNode;
                index--;
            }
            return KthNode.NodeValue;
        }
        public T FindMiddle()
        {
            Node<T>? fastNode = new Node<T>();
            Node<T>? slowNode = new Node<T>();
            fastNode = _tail.NextNode;
            slowNode = _tail.NextNode;

            // fastNode will traverse twice as fast as slowNode
            bool moveSlow = false;
            while (fastNode != null)
            {
                if (moveSlow) slowNode = slowNode.NextNode;
                fastNode = fastNode.NextNode;
                moveSlow = moveSlow ? false : true;
            }
            return slowNode.NodeValue;
        }
        public bool HasLoop()
        {
            Node<T>? fastNode = new Node<T>();
            Node<T>? slowNode = new Node<T>();
            fastNode = _tail.NextNode;
            slowNode = _tail.NextNode;

            // fastNode will traverse twice as fast as slowNode
            bool moveSlow = false;
            while (fastNode != null)
            {
                if (fastNode.NextNode == slowNode.NextNode)
                {
                    return true;
                }
                if (moveSlow) slowNode = slowNode.NextNode;
                fastNode = fastNode.NextNode;
                moveSlow = moveSlow ? false : true;
            }
            return false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Linked Lists using hand built library;
            LinkedList<int> linkedList = new LinkedList<int>();
            linkedList.AddFirst(1);
            linkedList.AddFirst(2);
            linkedList.AddFirst(3);
            linkedList.AddLast(4);
            linkedList.Print();
            linkedList.DeleteFirst();
            linkedList.Print();
            linkedList.DeleteLast();
            linkedList.Print();
            linkedList.DeleteLast();
            linkedList.Print();
            linkedList.DeleteLast();
            linkedList.Print();
            linkedList.DeleteLast();
            linkedList.Print();
            linkedList.DeleteFirst();
            linkedList.Print();
            linkedList.AddLast(4);
            linkedList.Print();
            linkedList.DeleteFirst();
            linkedList.AddFirst(1);
            linkedList.AddFirst(2);
            linkedList.AddFirst(3);
            linkedList.AddLast(4);
            linkedList.Print();
            Console.WriteLine($"Contains(4): {linkedList.Contains(4)}");
            Console.WriteLine($"Length: {linkedList.GetSize()}");
            int[] linkedListArray = linkedList.ToArray();
            Console.Write("Linked list to array: { ");
                
            for (int i = 0; i < linkedListArray.Length; i++)
            {
                Console.Write($"{linkedListArray[i]}, ");
            }
            Console.WriteLine(" }");
            linkedList.DeleteFirst();
            linkedList.Print();
            linkedList.DeleteLast();
            linkedList.Print();
            linkedList.Reverse();
            linkedList.Print();
            linkedList.AddFirst(3);
            linkedList.Print();
            linkedList.Reverse();
            linkedList.Print();
            Console.WriteLine($"GetKthNodeFromEnd(1): {linkedList.GetKthNodeFromEnd(1)}");
            Console.WriteLine($"FindMiddle: {linkedList.FindMiddle()}");
            linkedList.AddFirst(7);
            linkedList.AddFirst(8);
            linkedList.AddFirst(9);
            linkedList.Print();
            Console.WriteLine($"FindMiddle: {linkedList.FindMiddle()}");
        }
    }

}

