using System;
using System.ComponentModel;
using System.Globalization;

namespace Heaps
{
    class Heap
    {
        private readonly int _heapSize;
        private int _heapUsed;
        private readonly int[] _heapArray;
        public Heap(int size)
        {
            Console.WriteLine("Heap Constructed!");
            _heapSize = size;
            _heapUsed = 0;
            _heapArray = new int[_heapSize];
        }
        private void BubbleUp(int insertValue, int parentIndex, int childIndex)
        {
            if (_heapUsed < 1) return;
            var parentValue = _heapArray[parentIndex];
            if (insertValue <= parentValue) return;
            Console.WriteLine($"Bubbling up {insertValue} to {_heapArray[parentIndex]}");
            _heapArray[childIndex] = parentValue;
            _heapArray[parentIndex] = insertValue;
            BubbleUp(insertValue, ParentIndex(parentIndex), parentIndex);
        }
        private void BubbleDown(int index)
        {
            if (!HasChild(index))
            {
                ShiftLeftByOne(index, _heapUsed);
                return;
            }

            var largerChildIndex =
                (_heapArray[LeftChildIndex(index)] > _heapArray[RightChildIndex(index)])
                    ? LeftChildIndex(index)
                    : RightChildIndex(index);
            _heapArray[index] = _heapArray[largerChildIndex];
            BubbleDown(largerChildIndex);
        }
        private void ShiftLeftByOne(int startIndex, int endIndex)
        {
            for (var i = startIndex; i < endIndex; i++)
            {
                _heapArray[i] = _heapArray[i + 1];
            }
        }
        private bool HasChild(int index)
        {
            return (index * 2 + 1 < _heapUsed);
        }
        private static int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }
        private static int LeftChildIndex(int index)
        {
            return index * 2 + 1;
        }
        private static int RightChildIndex(int index)
        {
            return index * 2 + 2;
        }
        public void Insert(int value)
        {
            Console.WriteLine($"Inserting {value}");
            _heapArray[_heapUsed] = value;
            BubbleUp(value, ParentIndex(_heapUsed), _heapUsed);
            _heapUsed++;
        }
        public void Remove()
        {
            BubbleDown(0);
            _heapUsed--;
        }
        public int Size()
        {
            return _heapSize;
        }
        public int GetHeapUsed()
        {
            return _heapUsed;
        }
        public void Print()
        {
            foreach (var i in _heapArray)
            {
                Console.Write($"{i}, ");
            }

            Console.WriteLine();
        }
        public bool IsEmpty()
        {
            return (_heapUsed == 0);
        }
        public int Peek()
        {
            return _heapArray[0];
        }
    }
    class MinHeap
    {
        private class Node
        {
            public int Key { get; }
            public string Value { get; }

            public Node(int key, string value)
            {
                Key = key;
                Value = value;
            }
        }
        private readonly int _heapSize;
        private int _heapUsed;
        private readonly Node[] _heapArray;
        public MinHeap(int size)
        {
            Console.WriteLine("Min Heap Constructed");
            _heapSize = size;
            _heapUsed = 0;
            _heapArray = new Node[_heapSize];
        }
        private void BubbleUp(Node insertNode, int parentIndex, int childIndex)
        {
            if (_heapUsed < 1) return;
            var parentValue = _heapArray[parentIndex].Key;
            if (insertNode.Key <= parentValue) return;
            Console.WriteLine($"Bubbling up {insertNode.Key} to {_heapArray[parentIndex].Key}");
            _heapArray[childIndex] = _heapArray[parentIndex];
            _heapArray[parentIndex] = insertNode;
            BubbleUp(insertNode, ParentIndex(parentIndex), parentIndex);
        }
        private void BubbleDown(int index)
        {
            if (!HasChild(index))
            {
                ShiftLeftByOne(index, _heapUsed);
                return;
            }

            int RightChildKey = (_heapArray[RightChildIndex(index)] == null) ? 0 : _heapArray[RightChildIndex(index)].Key;

            var largerChildIndex =
                (_heapArray[LeftChildIndex(index)].Key > RightChildKey) ? 
                    LeftChildIndex(index) : RightChildIndex(index);
            _heapArray[index] = _heapArray[largerChildIndex];
            BubbleDown(largerChildIndex);
        }
        private void ShiftLeftByOne(int startIndex, int endIndex)
        {
            for (var i = startIndex; i < endIndex; i++)
            {
                _heapArray[i] = _heapArray[i + 1];
            }
        }
        private bool HasChild(int index)
        {
            return (index * 2 + 1 < _heapUsed);
        }
        private static int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }
        private static int LeftChildIndex(int index)
        {
            return index * 2 + 1;
        }
        private static int RightChildIndex(int index)
        {
            return index * 2 + 2;
        }
        public void Insert(int key, string value){
            Console.WriteLine($"Inserting ({key}: {value})");
            _heapArray[_heapUsed] = new Node(key, value);
            BubbleUp(_heapArray[_heapUsed], ParentIndex(_heapUsed), _heapUsed);
            _heapUsed++;
        }
        public void Remove()
        {
            BubbleDown(0);
            _heapUsed--;
        }
        public void Print()
        {
            for (int i = 0; i < _heapUsed; i++)
            {
                Console.WriteLine($"({_heapArray[i].Key}, {_heapArray[i].Value})");
            }
        }
        public bool IsEmpty()
        {
            return (_heapUsed == 0);
        }
        public (int, string) Peek()
        {
            return (_heapArray[0].Key, _heapArray[0].Value);
        }
    }
    class MinPriorityQueue
    {
        private MinHeap minHeap;
        public MinPriorityQueue(int size)
        {
            minHeap = new MinHeap(size);
        }
        public void Add(string value, int priority)
        {
            minHeap.Insert(priority, value);
        }
        public void Remove()
        {
            minHeap.Remove();
        }
        public (int, string) Peek()
        {
            return (minHeap.Peek().Item1, minHeap.Peek().Item2);
        }
        public bool IsEmpty()
        {
            return minHeap.IsEmpty();
        }
    }
    class HeapFunctions
    {
        public static void Heapify(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i * 2 + 1 > array.Length - 1) break;
                if (array[i] < array[i * 2 + 1])
                {
                    (array[i], array[i * 2 + 1]) = (array[i * 2 + 1], array[i]);
                }
                if (i * 2 + 2 > array.Length - 1) break;
                else if (array[i] < array[i * 2 + 2])
                {
                    (array[i], array[i * 2 + 2]) = (array[i * 2 + 2], array[i]);
                }
            }
        }
        public static int FindKthLargestItem(Heap heap, int k)
        {
            int KthLargest = 0;
            while (k > 0)
            {
                KthLargest = heap.Peek();
                heap.Remove();
                k--;
            }

            return KthLargest;
        }
        public static bool IsMaxHeap(Heap heap)
        {
            int previous = Int32.MaxValue;
            int current = 0;
            for (int i = 0; i < heap.GetHeapUsed(); i++)
            {
                current = heap.Peek();
                heap.Remove();
                if (previous < current) return false;
                previous = current;
            }

            return true;
        }

    }
    class Program
    {
        static void Main(string[] arg)
        {
            #region Heap Notes
            /*
             * Heaps are complete trees that are filled from left to right;
             * Every node is greater than or equal to it's children.  Heap property
             * Max heap: top most node is the biggest value
             * Min heap: top most node is the smallest value
             *
             * Applications:
             * - sorting
             * - graph algorithms
             * - priority queues
             * - finding the kth smallest/largest value
             *
             * - Heap generation process
             * Insert items in leaf until they are full.  If item is greater than parent,
             * then need to bubble up the item.  Bubbling is O(log(n)) operation.
             *
             * Heaps, even though they act as binary tree, can be built with array,
             * instead of using a separate node class.
             *
             * left = parent * 2 + 1;
             * right = parent * 2 + 2;
             * parent = (index - 1) / 2;
             *
             *          20[0]
             *'        /  \
             *       10[1] 15[2]
             *'     /   \
             *     4[3]  5[4]
             */
            #endregion
            #region CustomHeap
            //var myHeap = new Heap(10);
            //myHeap.Insert(20);
            //myHeap.Insert(10);
            //myHeap.Insert(15);
            //myHeap.Insert(4);
            //myHeap.Insert(5);
            //myHeap.Print();
            //myHeap.Insert(17);
            //myHeap.Print();
            //myHeap.Insert(25);
            //myHeap.Print();
            //myHeap.Remove();
            //myHeap.Print();
            #endregion
            #region HeapSort
            //int[] numbers = { 5, 3, 10, 1, 4, 2 };
            //var heap = new Heap(10);
            //foreach (var number in numbers)
            //{
            //    heap.Insert(number);
            //}

            //while (!heap.IsEmpty())
            //{
            //    Console.WriteLine(heap.Peek());
            //    heap.Remove();
            //}
            #endregion
            #region Heapify
            //Console.WriteLine("Heapify");
            //int[] numbers2 = { 5, 3, 8, 4, 1, 2 };
            //Heapify(numbers2);
            //foreach (var i in numbers2)
            //{
            //    Console.Write($"{i}, ");
            //}
            //Console.WriteLine();
            #endregion
            #region Kth Largest Item
            //int[] numbers3 = { 5, 3, 8, 4, 1, 2 };
            //var heap3 = new Heap(10);
            //foreach (var num in numbers3) heap3.Insert(num);
            //Console.WriteLine(HeapFunctions.FindKthLargestItem(heap3, 1));
            //Console.WriteLine(HeapFunctions.FindKthLargestItem(heap3, 1));
            //Console.WriteLine(HeapFunctions.FindKthLargestItem(heap3, 1));
            //Console.WriteLine();
            #endregion
            #region Is Max Heap
            //int[] numbers4 = { 5, 3, 8, 4, 1, 2 };
            //var heap4 = new Heap(10);
            //foreach (var num in numbers4) heap4.Insert(num);
            //Console.WriteLine(HeapFunctions.IsMaxHeap(heap4));
            #endregion
            #region MinHeap
            //var myHeap = new MinHeap(10);
            //myHeap.Insert(20, "1 - Hello");
            //myHeap.Insert(10, "2 - Michael");
            //myHeap.Insert(15, "3 - Sofia");
            //myHeap.Insert(4, "4 - Steve");
            //myHeap.Insert(5, "5 - Thomas");
            //myHeap.Insert(17, "6 - Alec");
            //myHeap.Print();
            //Console.WriteLine("Removed Top");
            //myHeap.Remove();
            //myHeap.Print();
            #endregion
            #region MinPriorityQueue
            //var heapQueue = new MinPriorityQueue(10);
            //heapQueue.Add("1 - Michael", 10);
            //heapQueue.Add("2 - Sofia", 7);
            //heapQueue.Add("3 - Juliana", 4);
            //heapQueue.Add("4 - Lisa", 9);
            //Console.WriteLine("Removing first in queue");
            //Console.WriteLine($"{heapQueue.Peek().Item1}, {heapQueue.Peek().Item2}");
            //heapQueue.Remove();
            #endregion
        }
    }
}
