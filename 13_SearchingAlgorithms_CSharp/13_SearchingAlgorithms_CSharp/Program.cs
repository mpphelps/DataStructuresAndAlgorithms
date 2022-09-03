using System;

namespace Searching_Algorithms
{
    public static class SearchingAlgorithms
    {
        public static int LinearSearch(int[] array, int target)
        {
            /*
             * Linear Search
             *  iteratively go through an array comparing each item
             *
             * Time Complexity: Best: O(1), Worst O(n)
             */

            for (var i = 0; i < array.Length; i++)
            {
                if (target == array[i]) return i;
            }

            return -1;
        }

        public static int BinarySearchRecursive(int[] array, int target)
        {
            /*
             * Binary Search
             *
             * only works on sorted arrays.
             * partition the array into halves, searching for the side
             *
             * Time Complexity: O(log n)
             */

            // Recursive approach
            var middle = array.Length / 2;
            if (target < array[0] || target > array[^1]) return -1;
            if (array[middle] == target) return middle;
            if (array[middle] < target) return BinarySearchRecursive(array, middle, array.Length, target);
            if (array[middle] > target) return BinarySearchRecursive(array, 0, middle, target);
            return -1;

            // Iterative approach
        }

        private static int BinarySearchRecursive(int[] array, int left, int right, int target)
        {
            var middle = (right + left) / 2;
            if (array[middle] == target) return middle;
            if (array[middle] < target) return BinarySearchRecursive(array, middle, right, target);
            if (array[middle] > target) return BinarySearchRecursive(array, left, middle, target);
            return -1;
        }

        public static int BinarySearchIterative(int[] array, int target)
        {
            var left = 0;
            var right = array.Length;
            var middle = array.Length / 2;
            
            if (target < array[0] || target > array[^1]) return -1;
            while (true)
            {
                if (array[middle] > target)
                {
                    right = middle;
                    middle = (right + left) / 2;
                }

                else if (array[middle] < target)
                {
                    left = middle;
                    middle = (right + left) / 2;
                }
                else return middle;
            }
        }

        public static int TernarySearchRecursive(int[] array, int target)
        {
            /*
             * Ternary Search
             *
             * Only works on sorted arrays.
             * Partition the array into thirds and search for which partition contains the target.
             * This is slower than binary search, i.e., if we did 10 partitions instead of three, we'd approach linear time complexity
             * 
             *
             * Time Complexity: O(log3 n)
             */

            if (target < array[0] || target > array[^1]) return -1;
            return TernarySearchRecursive(array, target, 0, array.Length);
        }

        private static int TernarySearchRecursive(int[] array, int target, int left, int right)
        {
            int partitionSize = (right - left) / 3;
            int mid1 = left + partitionSize;
            int mid2 = right - partitionSize;
            if (target > array[mid2]) return TernarySearchRecursive(array, target,mid2, right);
            if (target == array[mid2]) return mid2;
            if (target < array[mid2] && target > array[mid1]) return TernarySearchRecursive(array, target, mid1, mid2);
            if (target == array[mid1]) return mid1;
            if (target < array[mid1]) return TernarySearchRecursive(array, target, left, mid1);
            return -1;
        }

        public static int JumpSearch(int[] array, int target)
        {
            /*
             * Jump Search
             *
             * Only works on sorted arrays.
             * Partition the array into blocks, look at last item item in block, if target is greater than it, go to next block
             * This is faster than linear search, i.e., if we did 10 partitions instead of three, we'd approach linear time complexity
             * 
             *
             * Time Complexity: O(log3 n)
             */

            int blockSize = (int)Math.Sqrt(array.Length);
            int boxIndex = 0;

            while (target > array[boxIndex * blockSize])
            {
                boxIndex++;
                if (boxIndex > array.Length / blockSize) return -1;
            }
            int index = boxIndex * blockSize;
            while (target != array[index])
            {
                if (target > array[(boxIndex - 1) * blockSize]) return -1;
                index--;
            }

            return index;
        }

        public static int ExponentialSearch(int[] array, int target)
        {
            int bound = 1;
            if (target < array[0] || target > array[^1]) return -1;
            while (bound < array.Length && array[bound] < target)
                bound *= 2;
            int left = bound / 2;
            int right = Math.Min(bound, array.Length);
            return BinarySearchRecursive(array, target, left, right);
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            #region Search Parameters

            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var searchItem1 = 4;
            var searchItem2 = 90;
            var searchItem3 = 1;
            var searchItem4 = 10;

            #endregion

            #region Linear Search

            Console.WriteLine("Linear Search");
            Console.WriteLine($"Is {searchItem1} in array? {SearchingAlgorithms.LinearSearch(array, searchItem1)}");
            Console.WriteLine($"Is {searchItem2} in array? {SearchingAlgorithms.LinearSearch(array, searchItem2)}");
            Console.WriteLine($"Is {searchItem3} in array? {SearchingAlgorithms.LinearSearch(array, searchItem3)}");
            Console.WriteLine($"Is {searchItem4} in array? {SearchingAlgorithms.LinearSearch(array, searchItem4)}");

            #endregion

            #region BinarySearchRecursive

            Console.WriteLine("\nBinary Search Recursive");
            Console.WriteLine($"Is {searchItem1} in array? {SearchingAlgorithms.BinarySearchRecursive(array, searchItem1)}");
            Console.WriteLine($"Is {searchItem2} in array? {SearchingAlgorithms.BinarySearchRecursive(array, searchItem2)}");
            Console.WriteLine($"Is {searchItem3} in array? {SearchingAlgorithms.BinarySearchRecursive(array, searchItem3)}");
            Console.WriteLine($"Is {searchItem4} in array? {SearchingAlgorithms.BinarySearchRecursive(array, searchItem4)}");

            #endregion

            #region BinarySearchIterative

            Console.WriteLine("\nBinary Search Iterative");
            Console.WriteLine($"Is {searchItem1} in array? {SearchingAlgorithms.BinarySearchIterative(array, searchItem1)}");
            Console.WriteLine($"Is {searchItem2} in array? {SearchingAlgorithms.BinarySearchIterative(array, searchItem2)}");
            Console.WriteLine($"Is {searchItem3} in array? {SearchingAlgorithms.BinarySearchIterative(array, searchItem3)}");
            Console.WriteLine($"Is {searchItem4} in array? {SearchingAlgorithms.BinarySearchIterative(array, searchItem4)}");

            #endregion

            #region TernarySearchRecursive

            Console.WriteLine("\nTernary Search Recursive");
            Console.WriteLine($"Is {searchItem1} in array? {SearchingAlgorithms.TernarySearchRecursive(array, searchItem1)}");
            Console.WriteLine($"Is {searchItem2} in array? {SearchingAlgorithms.TernarySearchRecursive(array, searchItem2)}");
            Console.WriteLine($"Is {searchItem3} in array? {SearchingAlgorithms.TernarySearchRecursive(array, searchItem3)}");
            Console.WriteLine($"Is {searchItem4} in array? {SearchingAlgorithms.TernarySearchRecursive(array, searchItem4)}");

            #endregion

            #region JumpSearch

            Console.WriteLine("\nJump Search");
            Console.WriteLine($"Is {searchItem1} in array? {SearchingAlgorithms.JumpSearch(array, searchItem1)}");
            Console.WriteLine($"Is {searchItem2} in array? {SearchingAlgorithms.JumpSearch(array, searchItem2)}");
            Console.WriteLine($"Is {searchItem3} in array? {SearchingAlgorithms.JumpSearch(array, searchItem3)}");
            Console.WriteLine($"Is {searchItem4} in array? {SearchingAlgorithms.JumpSearch(array, searchItem4)}");

            #endregion

            #region ExponentialSearch

            Console.WriteLine("\nExponential Search");
            Console.WriteLine($"Is {searchItem1} in array? {SearchingAlgorithms.ExponentialSearch(array, searchItem1)}");
            Console.WriteLine($"Is {searchItem2} in array? {SearchingAlgorithms.ExponentialSearch(array, searchItem2)}");
            Console.WriteLine($"Is {searchItem3} in array? {SearchingAlgorithms.ExponentialSearch(array, searchItem3)}");
            Console.WriteLine($"Is {searchItem4} in array? {SearchingAlgorithms.ExponentialSearch(array, searchItem4)}");

            #endregion
        }
    }
}