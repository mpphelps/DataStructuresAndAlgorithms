#include <iostream>
#include <queue>
#include <stack>

template <typename T>
class ArrayQueue {
private:
    int _queueSize;
    int _front;
    int _back;
    T* _queue;
public:
    ArrayQueue(): _queueSize(10) 
    {
        _queue = new T[_queueSize];
        _back = _queueSize-1;
        _front = _queueSize-1;
        for (int i = 0; i < 10; i++)
        {
            _queue[i] = NULL;
        }
    }
    ArrayQueue(int size) : _queueSize(size)
    {
        _queue = new T[size];
        _back = size;
        _front = size;
        for (int i = 0; i < size; i++)
        {
            _queue[i] = NULL;
        }
    }
    ~ArrayQueue() {
        delete _queue;
    }
    void Enqueue(T value) {
        std::cout << "Queuing: " << value << std::endl;
        if (IsFull()) throw new std::invalid_argument("Queue is full");
        _queue[_back] = value;
        _back--;
        if (_back == -1) _back = _queueSize - 1;
    }
    void Dequeue() {
        std::cout << "Dequeuing front: " << Peek() << std::endl;
        if (IsEmpty()) throw new std::invalid_argument("Queue is empty");
        _queue[_front] = NULL;
        _front--;
        if (_back == -1) _back = _queueSize - 1;
    }
    T Peek() {
        return _queue[_queueSize-1];
    }
    bool IsEmpty() {
        for (int i = 0; i < _queueSize; i++) {
            if (_queue[i] != NULL) return false;
        }
        return true;
    }
    bool IsFull() {
        for (int i = 0; i < _queueSize; i++) {
            if (_queue[i] == NULL) return false;
        }
        return true;
    }
    int GetSize() {
        if (IsFull()) return _queueSize;
        else if (IsEmpty()) return 0;
        else if (_back>_front)
            return _front - (_back - _queueSize);
        else return _front - _back;
    }
    void Print() {
        std::cout << "Queue: {BACK:  ";
        int i = 1;
        while (i <= GetSize()) {
            if (_back + i >= _queueSize) { std::cout << _queue[(_back + i - _queueSize)] << ", "; }
            else { std::cout << _queue[(_back + i)] << ", "; }
            i++;
        }
        std::cout << " :FRONT}" << std::endl;
    }
};

template <typename T>
class StackQueue {
private:
    std::stack<T>* Stack1;
    std::stack<T>* Stack2;
public:
    StackQueue() {
        Stack1 = new std::stack<T>;
        Stack2 = new std::stack<T>;
    }
    ~StackQueue() {
        delete Stack1;
        delete Stack2;
    }
    void Enqueue(T value) {
        std::cout << "Queuing: " << value << std::endl;
        while (!Stack2->empty()) {
            Stack1->push(Stack2->top());
            Stack2->pop();
        }
        Stack1->push(value);
    }
    void Dequeue() {
        while (!Stack1->empty()) {
            Stack2->push(Stack1->top());
            Stack1->pop();
        }
        std::cout << "Dequeuing front: " << Stack2->top() << std::endl;
        Stack2->pop();
    }
    bool IsEmpty() {
    }
    bool IsFull() {
    }
    int GetSize() {
        (Stack1->size() > Stack2->size()) ? Stack1->size() : Stack2->size();
    }
    void Print() {
        std::cout << "BACK: { ";
        while (!Stack2->empty()) {
            Stack1->push(Stack2->top());
            Stack2->pop();
        }
        while (!Stack1->empty()) {
            std::cout << Stack1->top() << ", ";
            Stack2->push(Stack1->top());
            Stack1->pop();
        }
        std::cout << "} :FRONT";
    }
};

template <typename T>
class PriorityQueue {
private:
    int _queueSize;
    int _count;
    T* _priorityQueue;
public:
    PriorityQueue() : _queueSize(10), _count(0)
    {
        _priorityQueue = new T[_queueSize];
        for (int i = 0; i < 10; i++)
        {
            _priorityQueue[i] = NULL;
        }
    }
    PriorityQueue(int size) : _queueSize(size), _count(0)
    {
        _priorityQueue = new T[size];
        for (int i = 0; i < size; i++)
        {
            _priorityQueue[i] = NULL;
        }
    }
    ~PriorityQueue() {
        delete _priorityQueue;
    }
    void Enqueue(T value) {
        if (!IsFull()) {
            int i;
            for (i = _count-1; i >= 0; i--)
            {
                if (_priorityQueue[i] > value) {
                    _priorityQueue[i + 1] = _priorityQueue[i];
                }
                else {
                    break;
                }
            }
            _priorityQueue[i + 1] = value;
            _count++;
        }
    }
    void Dequeue() {
        if (!IsEmpty()) {
            _priorityQueue[_count-1] = NULL;
            _count--;
        }
    }
    T Peek() {
        return _priorityQueue[_count - 1];
    }
    void Print() {
        std::cout << "FRONT: {";
        for (int i = _count-1; i >= 0; i--)
        {
            std::cout << _priorityQueue[i] << ", ";
        }
        std::cout << " } :BACK" << std::endl;

    }
    bool IsFull() {
        if (_count == _queueSize) return true;
        else return false;
    }
    bool IsEmpty() {
        if (_count == 0) return true;
        else return false;
    }
};

template <typename T>
class LinkedListQueue
{
private:
    template <typename NodeType>
    struct Node
    {
    	NodeType value;
        Node* nextNode;
        Node() : value(0), nextNode(nullptr) {};
        Node(NodeType nodeValue) : value(nodeValue), nextNode(nullptr) {};
    };
    int _count;
    Node<T>* head = new Node<T>;
    Node<T>* tail = new Node<T>;
public:
	void Enqueue(T value)
	{
        Node<T>* newNode = new Node<T>(value);
		if(head->nextNode == nullptr && tail->nextNode == nullptr)
		{
            tail->nextNode = head->nextNode = newNode;
		}
		else
		{
            newNode->nextNode = tail->nextNode;
            tail->nextNode = newNode;
		}
        _count++;
	}
    void Dequeue()
	{
        Node<T>* nodePtr = tail->nextNode;
        while(nodePtr->nextNode->nextNode != nullptr)
        {
            nodePtr = nodePtr->nextNode;
        }
        head->nextNode = nodePtr;
        delete head->nextNode->nextNode;
        head->nextNode->nextNode = nullptr;
        _count--;
	}
    T Peek()
	{
        return head->nextNode;
	}
    int Size()
	{
        return _count;
	}
    bool IsEmpty()
	{
        if (_count == 0) return true;
        else return false;
	}
    void Print()
	{
        Node<T>* nodePtr = tail->nextNode;
        std::cout << "Queue: { ";
        while(nodePtr!=nullptr)
        {
            std::cout << nodePtr->value << ", ";
            nodePtr = nodePtr->nextNode;
        }
        std::cout << " }" << std::endl;
	}
};

void ReverseFirstKItems(int k, std::queue<int> &input)
{
    std::stack<int>* tempStack = new std::stack<int>;
    std::queue<int>* tempQueue = new std::queue<int>;
    int temp;

    for (int i = 0; i < k; i++)
    {
        tempStack->push(input.front());
        input.pop();
    }
    while(!input.empty())
    {
        tempQueue->push(input.front());
        input.pop();
    }
    while(!tempStack->empty())
    {
        input.push(tempStack->top());
        tempStack->pop();
    }
    while (!tempQueue->empty())
    {
        input.push(tempQueue->front());
        tempQueue->pop();
    }
}

void reverse(std::queue<int>* queue) {
    std::stack<int> stack;
    while (!queue->empty()) {
        stack.push(queue->front());
        queue->pop();
    }
    while (!stack.empty()) {
        std::cout << stack.top() << std::endl;
        stack.pop();
    }
}

void printFor() {
    int i;
    for (i = 5; i >= 0; i--)
    {
        std::cout << i << std::endl;
    }
    std::cout << i << std::endl;
}


int main()
{
    // Queue Notes
	/* Queues are Last in First out data structures
     * Enqueue - adding item to back
     * Dequeue - adding item from front
     * Peek - looking at item in front without removing from queue
     * IsEmpty
     * iIsFull
    */

    // Practice using queues from std library
    /*std::queue<int>* myQueue = new std::queue<int>;
    myQueue->push(10);
    myQueue->push(20);
    myQueue->push(30);
    myQueue->push(40);
    int i = 1;
    reverse(myQueue);*/

    // Practice using queues from custom ArrayQueue class
    /*ArrayQueue<int>* myQueue2 = new ArrayQueue<int>;
    myQueue2->Enqueue(1);
    myQueue2->Enqueue(2);
    myQueue2->Enqueue(3);
    myQueue2->Enqueue(4);
    myQueue2->Enqueue(5);
    myQueue2->Enqueue(6);
    myQueue2->Enqueue(7);
    myQueue2->Enqueue(8);
    myQueue2->Enqueue(9);
    myQueue2->Enqueue(10);
    std::cout << "Queue Size: " << myQueue2->GetSize() << std::endl;
    myQueue2->Print();
    myQueue2->Dequeue();
    myQueue2->Dequeue();
    myQueue2->Print();
    myQueue2->Enqueue(66);
    myQueue2->Enqueue(777);
    myQueue2->Print();*/

    // Practice using queues from custom StackQueue class
    /*StackQueue<int>* myQueue3 = new StackQueue<int>;
    myQueue3->Enqueue(10);
    myQueue3->Enqueue(20);
    myQueue3->Enqueue(30);
    myQueue3->Enqueue(40);
    myQueue3->Enqueue(50);
    myQueue3->Print();
    myQueue3->Dequeue();
    myQueue3->Dequeue();
    myQueue3->Dequeue();
    myQueue3->Print();
    myQueue3->Enqueue(30);
    myQueue3->Enqueue(40);
    myQueue3->Enqueue(50);
    myQueue3->Print();*/

    // Practice using priority queues from std library
    /*std::priority_queue<int>* priorityQueue = new std::priority_queue<int>;
    priorityQueue->push(5);
    priorityQueue->push(9);
    priorityQueue->push(1);
    priorityQueue->push(4);
    priorityQueue->push(2);
    std::cout << "Queue: { ";
    while (!priorityQueue->empty()) {
        std::cout << priorityQueue->top() << ", ";
        priorityQueue->pop();
    }
    std::cout << " }";*/

    // Practice using queues from custom priority queue class
    /*PriorityQueue<int>* priorityQueue = new PriorityQueue<int>;
    priorityQueue->Enqueue(5);
    priorityQueue->Enqueue(9);
    priorityQueue->Enqueue(1);
    priorityQueue->Enqueue(4);
    priorityQueue->Enqueue(2);
    priorityQueue->Print();
    priorityQueue->Dequeue();
    priorityQueue->Dequeue();
    priorityQueue->Dequeue();
    priorityQueue->Print();
    priorityQueue->Enqueue(30);
    priorityQueue->Enqueue(40);
    priorityQueue->Enqueue(50);
    priorityQueue->Print();*/

    // Exercises for Queues
    // Problem 1
    /*auto myQueue = new std::queue<int>;
    myQueue->push(10);
    myQueue->push(20);
    myQueue->push(30);
    myQueue->push(40);
    myQueue->push(50);
    ReverseFirstKItems(3, *myQueue);
    int queueSize = myQueue->size();
    for( int i = 0; i < queueSize; i++)
    {
        std::cout << myQueue->front() << std::endl;
        myQueue->pop();
    }*/

    // Practice using queues from custom linked list queue class
	auto* linkListQueue = new LinkedListQueue<int>;
    linkListQueue->Enqueue(10);
    linkListQueue->Enqueue(20);
    linkListQueue->Enqueue(30);
    linkListQueue->Enqueue(40);
    linkListQueue->Enqueue(50);
    linkListQueue->Print();
    linkListQueue->Dequeue();
    linkListQueue->Dequeue();
    linkListQueue->Print();
}
