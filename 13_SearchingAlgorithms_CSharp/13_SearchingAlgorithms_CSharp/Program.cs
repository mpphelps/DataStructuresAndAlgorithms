using System;

namespace Searching_Algorithms
{
    public static class SearchingAlgorithms
    {
        public static int LinearSearch(int[] array, int searchItem)
        {
            /*
             * Linear Search
             *  iteratively go through an array comparing each item
             *
             * Time Complexity: Best: O(1), Worst O(n)
             */

            for (var i = 0; i < array.Length; i++)
            {
                if (searchItem == array[i]) return i;
            }

            return -1;
        }

        public static int BinarySearchRecursive(int[] array, int searchItem)
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
            if (searchItem < array[0] || searchItem > array[^1]) return -1;
            if (array[middle] == searchItem) return middle;
            if (array[middle] < searchItem) return BinarySearchRecursive(array, middle, array.Length, searchItem);
            if (array[middle] > searchItem) return BinarySearchRecursive(array, 0, middle, searchItem);
            return -1;

            // Iterative approach
        }

        private static int BinarySearchRecursive(int[] array, int left, int right, int searchItem)
        {
            var middle = (right + left) / 2;
            if (array[middle] == searchItem) return middle;
            if (array[middle] < searchItem) return BinarySearchRecursive(array, middle, right, searchItem);
            if (array[middle] > searchItem) return BinarySearchRecursive(array, left, middle, searchItem);
            return -1;
        }

        public static int BinarySearchIterative(int[] array, int searchItem)
        {
            var left = 0;
            var right = array.Length;
            var middle = array.Length / 2;
            
            if (searchItem < array[0] || searchItem > array[^1]) return -1;
            while (true)
            {
                if (array[middle] > searchItem)
                {
                    right = middle;
                    middle = (right + left) / 2;
                }

                else if (array[middle] < searchItem)
                {
                    left = middle;
                    middle = (right + left) / 2;
                }
                else return middle;
            }
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            var array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var searchItem1 = 4;
            var searchItem2 = 90;
            var searchItem3 = 1;
            var searchItem4 = 10;

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
        }
    }
}