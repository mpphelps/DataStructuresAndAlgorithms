using System;

namespace Searching_Algorithms
{
    class SearchingAlgorithms
    {
        public static bool LinearSearch(int[] array, int searchItem)
        {
            /*
             * Linear Search
             *  iteratively go through an array comparing each item
             *
             * Time Complexity: Best: O(1), Worst O(n)
             */

            foreach (var item in array)
            {
                if (searchItem == item) return true;
            }
            return false;
        }
    }
    class Program
    {
        public static void Main(string[] args)
        {
            #region Linear Search

            Console.WriteLine("Linear Search");
            var array1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var searchItem1 = 4;
            var searchItem2 = 90;
            Console.WriteLine($"Is {searchItem1} in array? {SearchingAlgorithms.LinearSearch(array1, searchItem1)}");
            Console.WriteLine($"Is {searchItem2} in array? {SearchingAlgorithms.LinearSearch(array1, searchItem2)}");

            #endregion
        }
    }
}