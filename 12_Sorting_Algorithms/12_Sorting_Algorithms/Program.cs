using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Timers;

namespace SortingAlgorithms
{
    public static class SortingAlgorithms
    {
        public static void BubbleSort(int[] array)
        {
            /* Bubble Sort
             *  Compare array[i] and array[i-1].  If array[i-1] is larger then swap values.
             *  This process will bubble up highest value to the end of array, so decrement
             *  left limit by one and repeat process.
             *
             *  Time Complexity O(n^2)
             */
            bool isSorted;
            for (var i = 0; i < array.Length; i++)
            {
                isSorted = true;
                for (var j = 1; j < array.Length; j++)
                {
                    if (array[j - 1] > array[j])
                    {
                        SortingAlgorithms.Swap(array, j, j - 1);
                        isSorted = false;
                    }
                }

                if (isSorted) return;
                //Console.WriteLine(string.Join(", ", array));
            }
        }
        public static void SelectionSort(int[] array)
        {
            /* Selection Sort
             *  Looks for the minimum value and then swaps the value with value at left limit.
             *  Increment left limit by one and repeat process.
             *
             *  Time Complexity O(n^2)
             */
            for (var i = 0; i < array.Length; i++)
            {
                var min = int.MaxValue;
                var minIndex = i;
                for (var j = i + 1; j < array.Length; j++)
                    if (array[j] < array[minIndex])
                        minIndex = j;
                SortingAlgorithms.Swap(array, minIndex, i);
                //Console.WriteLine(string.Join(", ", array));
            }
        }
        public static void InsertionSort(int[] array)
        {
            /* Insertion Sort
             *  Takes one item at a time, and then inserts it back in place, like a deck of cards
             *
             *  Time Complexity O(n^2)
             */
            for (var i = 0; i < array.Length; i++)
            {
                var current = array[i];
                var j = i - 1;
                while (j >= 0 && array[j] > current)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = current;
                //Console.WriteLine(string.Join(", ", array));
            }
        }
        public static void MergeSort(int[] array)
        {
            /* Merge Sort
             *  Break the array in half recursively until it's individual elements.
             *  Then recombine using merge pointers
             *
             *  Time Complexity: O(n log n)
             */

            if (array.Length < 2) return;
            var middleIndex = array.Length / 2;
            //Console.WriteLine(string.Join(", ", array[0..middleIndex]));
            //Console.WriteLine(string.Join(", ", array[middleIndex..^0]));
            var left = array[0..middleIndex];
            var right = array[middleIndex..^0];
            MergeSort(left);
            MergeSort(right);

            Merge(left, right, array);
            //Console.WriteLine(string.Join(", ", array));
        }
        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }
        public static void CountingSort(int[] array)
        {
            /* Counting Sort
             *  Creates an array that tracks total occurrences of values in an array.
             *  Occurrences are stored in an array and then iterated over to repopulate the array
             *
             * Time Complexity: O(n)
             *
             * Downside is it can potentially have a huge memory trade off if any value in array is large
             */
            var occurrences = new int[array.Max() + 1];
            foreach (var i in array)
                occurrences[i]++;

            int j = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (occurrences[j] > 0)
                {
                    array[i] = j;
                    occurrences[j]--;
                }
                else if (occurrences[j] == 0)
                {
                    i--;
                    j++;
                }

            }
        }
        public static void BucketSort(int[] array, int numOfBuckets)
        {
            /* Bucket Sort
             *  Use buckets to split up array and then sort those buckets
             *
             * Time Complexity: O(n) to O(n^2)
             *
             */
            var buckets = new List<LinkedList<int>>(numOfBuckets);
            for (var i = 0; i < numOfBuckets; i++)
                buckets.Add(new LinkedList<int>());
            foreach (var t in array)
                buckets[t / numOfBuckets].AddLast(t);
            var j = 0;
            foreach (var bucket in buckets)
            {
                var orderedEnumerable = bucket.OrderBy(x => x);
                foreach (var item in orderedEnumerable)
                {
                    array[j++] = item;
                }
            }
        }
        private static void QuickSort(int[] array, int start, int end)
        {
            /* Quick Sort
             *  Start by selecting a value for the pivot, and then making all
             *  items less than the pivot be on the left side of it, and
             *  similarly for items greater than the pivot.  Typically
             *  the last item is selected as the pivot.  Can also pick randomly.
             *
             * Time Complexity: 
             */

            // Need two pointers to iterate over this array, i and b (boundary, end of left partition)

            if (start >= end) return;
            var boundary = Partition(array, start, end);
            QuickSort(array, start, boundary - 1);
            QuickSort(array, boundary + 1, end);

        }
        private static int Partition(int[] array, int start, int end)
        {
            var pivot = array[end];
            var boundary = start - 1;
            for (var i = start; i <= end; i++)
                if (array[i] <= pivot)
                    Swap(array, i, ++boundary);

            return boundary;
        }
        private static void Merge(int[] left, int[] right, int[] result)
        {
            // merge the two arrays
            // if left is less than right, then result == left, move along
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                    result[k++] = left[i++];
                else
                    result[k++] = right[j++];
            }

            while (i < left.Length)
                result[k++] = left[i++];

            while (j < right.Length)
                result[k++] = right[j++];

        }
        private static void Swap(int[] array, int index1, int index2)
        {
            (array[index1], array[index2]) = (array[index2], array[index1]);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Bubble Sort
            Console.WriteLine("Bubble Sort");
            var myArray1 = new[] { 5, 7, 3, 4, 9, 1, 2, 0, 6, 8 };
            //var myArray1 = new int[] { 7, 5};
            //var myArray1 = new int[] { };
            Console.WriteLine(string.Join(", ", myArray1));
            SortingAlgorithms.BubbleSort(myArray1);
            Console.WriteLine(string.Join(", ", myArray1));
            #endregion
            #region Selection Sort
            Console.WriteLine("Selection Sort");
            var myArray2 = new[] { 5, 7, 3, 4, 9, 1, 2, 0, 6, 8 };
            //var myArray2 = new int[] { 7, 5};
            //var myArray2 = new int[] { };
            Console.WriteLine(string.Join(", ", myArray2));
            SortingAlgorithms.SelectionSort(myArray2);
            Console.WriteLine(string.Join(", ", myArray2));
            #endregion
            #region Insertion Sort
            Console.WriteLine("Insertion Sort");
            var myArray3 = new[] { 5, 7, 3, 4, 9, 1, 2, 0, 6, 8 };
            //var myArray3 = new int[] { 7, 5};
            //var myArray3 = new int[] { };
            Console.WriteLine(string.Join(", ", myArray3));
            SortingAlgorithms.InsertionSort(myArray3);
            Console.WriteLine(string.Join(", ", myArray3));
            #endregion
            #region Merge Sort
            Console.WriteLine("Merge Sort");
            var myArray4 = new[] { 5, 7, 3, 4, 9, 1, 2, 0, 6, 8 };
            //var myArray4 = new int[] { 7, 5};
            //var myArray4 = new int[] { };
            Console.WriteLine(string.Join(", ", myArray4));
            SortingAlgorithms.MergeSort(myArray4);
            Console.WriteLine(string.Join(", ", myArray4));
            #endregion
            #region Quick Sort
            Console.WriteLine("Quick Sort");
            var myArray5 = new[] { 5, 7, 3, 4, 9, 1, 2, 0, 6, 8 };
            //var myArray5 = new int[] { 7, 5};
            //var myArray5 = new int[] { };
            Console.WriteLine(string.Join(", ", myArray5));
            SortingAlgorithms.QuickSort(myArray5);
            Console.WriteLine(string.Join(", ", myArray5));
            #endregion
            #region Counting Sort
            Console.WriteLine("Counting Sort");
            var myArray6 = new[] { 5, 7, 3, 4, 9, 1, 2, 0, 6, 8 };
            //var myArray6 = new int[] { 7, 5};
            //var myArray6 = new int[] { };
            Console.WriteLine(string.Join(", ", myArray6));
            SortingAlgorithms.CountingSort(myArray6);
            Console.WriteLine(string.Join(", ", myArray6));
            #endregion
            #region Bucket Sort
            Console.WriteLine("Bucket Sort");
            var myArray7 = new[] { 5, 7, 3, 4, 9, 1, 2, 0, 6, 8 };
            //var myArray7 = new int[] { 7, 5};
            //var myArray7 = new int[] { };
            Console.WriteLine(string.Join(", ", myArray7));
            SortingAlgorithms.BucketSort(myArray7, 4);
            Console.WriteLine(string.Join(", ", myArray7));
            #endregion
        }
    }
}

