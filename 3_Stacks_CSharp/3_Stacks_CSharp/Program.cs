using System;

namespace Stacks
{
    class Stack<T> where T : IComparable
    {
        private int _count = 0;
        private T[]? _stack;
        private int _stackSize;
        public Stack()
        {
            _stack = new T[0];
        }
        public Stack(int size)
        {
            _stackSize = size;
            _stack = new T[size];
        }
        private bool IsEmpty()
        {
            if (_count == 0) return true;
            else return false;
        }
        public void Push(T item)
        {
            if (_count == _stackSize)
            {
                T[] stack2 = new T[_stackSize + 50];
                for (int i = 0; i < _stack.Length; i++)
                {
                    stack2[i] = _stack[i];
                }
                stack2[_stackSize] = item;
                _stack = null;
                _stack = stack2;
                _stackSize += _stackSize + 50;
                _count++;
            }
            else
            {
                _stack[_count] = item;
                _count++;
            }
        }
        public void Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty)");
            else _stack[_count - 1] = default(T);
            _count--;
        }
        public T Peek()
        {
            return _stack[_count - 1];
        }
        public void Print()
        {
            Console.Write("Stack: { ");
            for (int i = 0; i < _count; i++)
            {
                Console.Write($"{_stack[i]}, ");
            }
            Console.WriteLine(" }");
        }
        public T Min()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty)");
            T min = _stack[0];
            for (int i = 0; i < _count; i++)
            {
                if (min.CompareTo(_stack[i]) >= 0) min = _stack[i];
            }
            return min;
        }
        public T Max()
        {
            if (IsEmpty()) throw new InvalidOperationException("Stack is empty)");
            T max = _stack[0];
            for (int i = 0; i < _count; i++)
            {
                if (max.CompareTo(_stack[i]) <= 0) max = _stack[i];
            }
            return max;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> myStack2 = new Stack<int>();
            myStack2.Push(2);
            myStack2.Push(3);
            myStack2.Push(8);
            myStack2.Push(6);
            myStack2.Push(4);
            myStack2.Push(1);
            myStack2.Print();
            Console.WriteLine($"Stack Top: {myStack2.Peek()}");
            myStack2.Pop();
            myStack2.Pop();
            myStack2.Print();
            Console.WriteLine($"Stack Min: {myStack2.Min()}");
            Console.WriteLine($"Stack Max: {myStack2.Max()}");
        }
    }
}

