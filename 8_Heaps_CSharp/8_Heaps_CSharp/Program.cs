using System;

namespace Heaps
{
    class Heap
    {
        private readonly int _heapSize;
        private int _heapUsed;
        private readonly int[] _heapArray;
        public Heap(int size)
        {
            _heapSize = size;
            _heapUsed = 0;
            _heapArray = new int[_heapSize];
        }
        private void BubbleUp(int insertValue, int parentIndex, int childIndex)
        {
            if (_heapUsed < 1) return;
            int parentValue = _heapArray[parentIndex];
            if (insertValue > parentValue)
            {
                Console.WriteLine($"Bubbling up {insertValue} to {_heapArray[parentIndex]}");
                _heapArray[childIndex] = parentValue;
                _heapArray[parentIndex] = insertValue;
                BubbleUp(insertValue, (parentIndex - 1) / 2, parentIndex);
            }
        }
        private void BubbleDown(int index)
        {
            if (!HasChild(index))
            {
                for (int i = index; i < _heapUsed; i++)
                {
                    _heapArray[i] = _heapArray[i + 1];
                }
                return;
            };
            int leftChildIndex = index * 2 + 1;
            int rightChildIndex = index * 2 + 2;
            if (_heapArray[leftChildIndex] > _heapArray[rightChildIndex])
            {
                _heapArray[index] = _heapArray[leftChildIndex];
                BubbleDown(leftChildIndex);
            }
            else
            {
                _heapArray[index] = _heapArray[rightChildIndex];
                BubbleDown(rightChildIndex);
            }
        }
        private bool HasChild(int index)
        {
            return (index * 2 + 1 < _heapUsed);
        }
        public void Insert(int value)
        {
            Console.WriteLine($"Inserting {value}");
            _heapArray[_heapUsed] = value;
            BubbleUp(value, (_heapUsed - 1) / 2, _heapUsed);
            _heapUsed++;
        }
        public void Remove(int value)
        {   //TODO Improve to OLogN operation
            for (int i = 0; i < _heapUsed; i++)
            {
                if (_heapArray[i] == value)
                {
                    BubbleDown(i);
                    _heapUsed--;
                    break;
                }
            }
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

            var myHeap = new Heap(10);
            myHeap.Insert(20);
            myHeap.Insert(10);
            myHeap.Insert(15);
            myHeap.Insert(4);
            myHeap.Insert(5);
            myHeap.Print();
            myHeap.Insert(17);
            myHeap.Print();
            myHeap.Insert(25);
            myHeap.Print();
            myHeap.Remove(10);
            myHeap.Print();

        }
    }
}
