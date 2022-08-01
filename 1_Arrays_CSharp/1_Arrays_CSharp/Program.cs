using System;

namespace Arrays
{

    class Array<T> where T : IComparable
    {
        private int _count = 0;
        private T[] _array;
        private int _arraySize;

        public Array()
        {
            _count = 0;
            _array = new T[0];
            _arraySize = 0;
        }
        public int GetCount() { return _count; }
        public void Print()
        {
            for (int i = 0; i < _count; i++)
            {
                Console.WriteLine(_array[i]);
            }
        }
        public void Insert(T element)
        {
            if (_count == _arraySize)
            {
                T[] array2 = new T[_arraySize + 1];
                for (int i = 0; i < _arraySize; i++)
                {
                    array2[i] = _array[i];
                }
                array2[_arraySize] = element;
                _array = array2;
                _arraySize++;
                _count++;
            }
            else
            {
                _array[_count] = element;
                _count++;
            }
        }
        public void InsertAt(T element, int index)
        {
            if (_count == _arraySize)
            {
                T[] array2 = new T[_arraySize + 1];
                for (int i = 0; i < _arraySize; i++)
                {
                    array2[i] = _array[i];
                }
                array2[_arraySize] = element;
                _array = array2;
                _arraySize++;
                _count++;
            }
            for (int i = 0; i < _count; i++)
            {
                _array[i+1] = _array[i];
            }
            _array[index] = element;
            _count++;
        }
        public void RemoveAt(int index)
        {
            try
            {
                if (index < 0 || index >= _count) throw new InvalidOperationException("Invalid Index");
                Console.WriteLine($"Removing: array[{index}] = {_array[index]}");
                for (int i = index; i < _count; i++)
                {
                    _array[index] = _array[index + 1];
                }
                _count--;
            }
            catch
            {
                Console.WriteLine($"Invalid index: {index}");
            }
        }
        public T ElementAt(int index)
        {
            try
            {
                return _array[index];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public int indexOf(T? element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Looking for a null element");
            }
            else
            {
                for (int i = 0; i < _count; i++)
                {
                    if (_array[i].Equals(element)) return i;
                }
                return -1;
            }
        }
        public bool Contains(T? element)
        {
            for (int i = 0; i < _count; i++)
                if (_array[i].Equals(element)) return true;
            return false;
        }
        // TO DO: Figure out how to compare elements of Type T
        public T Max()
        {
            T max = _array[0];
            for (int i = 0; i < _count; i++)
            {
                if (max.CompareTo(_array[i]) >= 0) max = _array[i];
            }
            return max;
        }
        public T Min()
        {
            T min = _array[0];
            for (int i = 0; i < _count; i++)
            {
                if (min.CompareTo(_array[i]) <= 0) min = _array[i];
            }
            return min;
        }
        void Reverse()
        {
            Console.WriteLine("Reversing Array");
            T[] array2 = new T[_arraySize];
            for (int i = 0; i < _count; i++)
            {
                array2[i] = _array[_count - 1 - i];
            }
            _array = array2;
        }
        Array<T> Intersect(Array<T> comparedWithArray)
        {
            var intersectionResultArray = new Array<T>();
            for (int i = 0; i < this.GetCount(); i++)
            {
                if (comparedWithArray.Contains(_array[i]))
                {
                    intersectionResultArray.Insert(_array[i]);
                }
            }
            return intersectionResultArray;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Array<int> nums = new Array<int>();
            nums.Insert(3);
            nums.Insert(5);
            nums.Insert(8);
            nums.Insert(1);
            nums.Insert(3);
            nums.Print();
            Console.WriteLine($"Max: {nums.Max()}, Min: {nums.Min()}");
        }
    }
}


