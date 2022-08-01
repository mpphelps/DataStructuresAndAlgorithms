#include <iostream>
#include <stack>
#include <array>

template <typename T>
class Stack {
private:
	int _count = 0;
	T* _stack;
	int _stackSize;
public:

	Stack() : _stackSize(0) {
		_stack = new T[0];
	}
	Stack(const int size) : _stackSize(size) {
		_stack = new T[size];
	}
	~Stack() {
		delete _stack;
	}
	void Push(T item) {
		if (_count == _stackSize) {
			T* stack2 = new T[_stackSize + 50];
			for (int i = 0; i < _stackSize; i++)
			{
				stack2[i] = _stack[i];
			}
			stack2[_stackSize] = item;
			delete[] _stack;
			_stack = stack2;
			_stackSize += _stackSize + 50;
			_count++;
		}
		else {
			_stack[_count] = item;
			_count++;
		}
	}
	void Pop() {
		if (this->IsEmpty()) throw new std::invalid_argument("Stack is empty)");
		else _stack[_count - 1] = 0;
		_count--;
	}
	T Peek() {
		return _stack[_count - 1];
	}
	bool IsEmpty() {
		if (_count == 0) return true;
		else return false;
	}
	void Print() {
		std::cout << "Stack: { ";
		for (int i = 0; i < _count; i++)
		{
			std::cout << _stack[i] << ", ";
		}
		std::cout << " }" << std::endl;
	}
	T Min() {
		if (this->IsEmpty()) throw new std::logic_error("Stack is empty)");
		T min = _stack[0];
		for (int i = 0; i < _count; i++)
		{
			if (_stack[i] < min) min = _stack[i];
		}
		return min;
	}
	T Max() {
		if (this->IsEmpty()) throw new std::logic_error("Stack is empty)");
		T max = _stack[0];
		for (int i = 0; i < _count; i++)
		{
			if (_stack[i] > max) max = _stack[i];
		}
		return max;
	}
};

class StringReverser {

public:
	static void ReverseString(std::string str) {
		if (&str == NULL) throw new std::invalid_argument("Input String can not but null");
		std::stack<char> strStack;
		for (char ch : str) {
			strStack.push(ch);
		}
		while (!strStack.empty()) {
			std::cout << strStack.top();
			strStack.pop();
		}
		std::cout << std::endl;
	}
	static bool BalancedExpressionChecker(std::string str) {
		if (&str == NULL) throw new std::invalid_argument("Input String can not but null");
		std::stack<char> strStack;
		for (char ch : str) {
			switch (ch)
			{
			case '(':
			case '[':
			case '{':
			case '<':
				strStack.push(ch);
				break;
			case ')':
				if (strStack.empty() || strStack.top() != '(') return false;
				else (strStack.pop());
				break;
			case ']':
				if (strStack.empty() || strStack.top() != '[') return false;
				else (strStack.pop());
				break;
			case '}':
				if (strStack.empty() || strStack.top() != '{') return false;
				else (strStack.pop());
				break;
			case '>':
				if (strStack.empty() || strStack.top() != '<') return false;
				else (strStack.pop());
				break;
			default:
				break;
			}
		}
		return true;
	}
};

int main()
{
	/* Stacks are Last in First out(LIFO)
	*
	* Stack operations
	*	Push(item) - adds itemt o stack
	*	Pop() - removes item from stack
	*	Peek() - returns item on top without removing from stack
	*	IsEmpty() - returns true if stack is empty
	*
	* All Operations in stack operate in O(1) complexity
	*/

	std::stack<int> myStack;
	myStack.push(10);
	myStack.push(20);
	myStack.push(30);
	myStack.push(40);
	std::cout << &myStack << std::endl;
	std::string str = "Hello!";
	StringReverser::ReverseString(str);
	std::string str1 = ")(([1] + <2>)[a]";
	std::string str2 = "((<1] + <2>)[a]";
	std::cout << "str1 is balanced?: " << (StringReverser::BalancedExpressionChecker(str1) ? "True" : "False") << std::endl;
	std::cout << "str2 is balanced?: " << (StringReverser::BalancedExpressionChecker(str2) ? "True" : "False") << std::endl;

	Stack<int> myStack2;
	myStack2.Push(2);
	myStack2.Push(3);
	myStack2.Print();
	std::cout << "Stack Top: " << myStack2.Peek() << std::endl;
	myStack2.Pop();
	myStack2.Pop();
	myStack2.Print();

	return 0;

}
