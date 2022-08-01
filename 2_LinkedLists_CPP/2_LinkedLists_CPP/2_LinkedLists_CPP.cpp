#include <iostream>
#include <list>

template <typename T>
class LinkedList 
{
private:
    template <typename T>
    struct Node
    {
        T nodeValue;
        Node* nextNode;
        Node() : nodeValue(0), nextNode(nullptr) {};
        Node(T value) : nodeValue(value), nextNode(nullptr) {};
        Node(T value, Node* next) : nodeValue(value), nextNode(next) {};
    };
    Node<T>* head;
    Node<T>* tail;
    int size;
    bool isEmpty() {
        if (tail->nextNode == nullptr && head->nextNode == nullptr) return true;
        else return false;
    }

public:
    LinkedList():size(0) {
        head = new Node<T>();
        tail = new Node<T>();
    }
    void AddFirst(T value)
    {
        Node<T>* node = new Node<T>(value);
        if (isEmpty()) {
            tail->nextNode = node;
            head->nextNode = node;
        }
        else
        {
            node->nextNode = tail->nextNode;
            tail->nextNode = node;
        }
        size++;
    }
    void AddLast(T value)
    {
        Node<T>* node = new Node<T>(value);
        if (isEmpty()) {
            tail->nextNode = node;
            head->nextNode = node;
        }
        else
        {
            head->nextNode->nextNode = node;
            head->nextNode = node;
        }
        size++;
    }
    void DeleteFirst()
    {
        Node<T>* tempNode = new Node<T>();
        tempNode->nextNode = tail->nextNode;
        if (isEmpty())
        {
            return;
        }
        else if (GetSize() == 1)
        {
            tail->nextNode = nullptr;
            head->nextNode = nullptr;
            delete tempNode->nextNode;
        }
        else 
        {
            tail->nextNode = tail->nextNode->nextNode;
            delete tempNode->nextNode;
        }
        size--;
        std::cout << "Deleted First Node" << std::endl;
    }
    void DeleteLast()
    {
        Node<T>* tempNode = new Node<T>();
        tempNode->nextNode = tail->nextNode;
        if (isEmpty())
        {
            return;
        }
        else if (GetSize()==1)
        {
            tail->nextNode = nullptr;
            head->nextNode = nullptr;
            delete tempNode->nextNode;
        }
        else
        {
            while (tempNode->nextNode->nextNode != nullptr) 
            {
                tempNode = tempNode->nextNode;
                if (tempNode->nextNode->nextNode == nullptr)
                {
                    head->nextNode = tempNode;
                    tempNode->nextNode = nullptr;
                    break;
                }
            }
            delete tempNode->nextNode;
        }
        size--;
        std::cout << "Deleted Last Node" << std::endl;
    }
    bool Contains(T element)
    {
        if (tail->nextNode != nullptr) {
            Node<T>* nodePtr = tail->nextNode;
            while (true) 
            {
                if (nodePtr->nodeValue == element) return true;
                else if (nodePtr->nextNode == nullptr) return false;
                else nodePtr = nodePtr->nextNode;
            }
        }
        return false;
    }
    int IndexOf(T element)
    {
        int index = 0;
        if (tail->nextNode != nullptr) {
            Node<T>* nodePtr = tail->nextNode;
            while (true)
            {
                if (nodePtr->nodeValue == element) return index;
                else if (nodePtr->nextNode == nullptr) return index;
                else 
                {
                    nodePtr = nodePtr->nextNode;
                    index++;
                }
            }
        }
        return -1;
    }
    void Print(){
        Node<T>* nodePtr = tail->nextNode;
        std::cout << "LinkedList = { ";
        if (nodePtr != NULL) {
            while (nodePtr->nextNode != nullptr) {
                std::cout << nodePtr->nodeValue << ", ";
                nodePtr = nodePtr->nextNode;
            }
            std::cout << nodePtr->nodeValue;
        }
        std::cout << " }\n";
    }
    int GetSize()
    {
        return size;
    }
    void ToArray(T* myArray) {
        if (isEmpty()) return;
        else {
            Node<T>* tempNode = new Node<T>();
            int index = 0;
            tempNode = tail;
            while (tempNode->nextNode != nullptr)
            {
                tempNode = tempNode->nextNode;
                myArray[index] = tempNode->nodeValue;
                index++;
            }
        }
    }
    void Reverse()
    {
        if (size == 0 || size == 1) {/*do nothing*/}
        else if (size == 2)
        {
            Node<T>* tempNode = new Node<T>();
            head->nextNode->nextNode = tail->nextNode;
            tail->nextNode->nextNode = nullptr;
            tempNode->nextNode = head->nextNode;
            head->nextNode = tail->nextNode;
            tail->nextNode = tempNode->nextNode;
            delete tempNode;
        }
        else
        {
            // Allocate three temps nodes on the heap
            Node<T>* previousNode = new Node<T>();
            Node<T>* currentNode = new Node<T>();
            Node<T>* nextNode = new Node<T>();

            // Set the temp nodes to first three nodes in list
            previousNode->nextNode  = tail->nextNode;
            currentNode->nextNode   = tail->nextNode->nextNode;
            nextNode->nextNode      = tail->nextNode->nextNode->nextNode;

            // Change the first nodes nextnode to null and move the head;
            previousNode->nextNode->nextNode = nullptr;
            head->nextNode = tail->nextNode;
            
            while (nextNode->nextNode != nullptr) {
                // Change node2 next node to previous node
                currentNode->nextNode->nextNode = previousNode->nextNode;
                previousNode->nextNode = currentNode->nextNode;
                currentNode->nextNode = nextNode->nextNode;
                nextNode->nextNode = nextNode->nextNode->nextNode;
            }
            // Change the last nodes nextNode and move the tail
            currentNode->nextNode->nextNode = previousNode->nextNode;
            tail->nextNode = currentNode->nextNode;

            // Delete temp nodes from heap
            delete previousNode;
            delete currentNode;
            delete nextNode;
        }
    }
    T GetKthNodeFromEnd(int index) {
        if (index > size || index < 0) 
            throw new std::invalid_argument("Invalid index");
        index = size - index;
        Node<T>* KthNode = new Node<T>;
        KthNode->nextNode = tail->nextNode;
        while (index) {
            KthNode = KthNode->nextNode;
            index--;
        }
        return KthNode->nodeValue;
    }
    T FindMiddle() {
        Node<T>* fastNode = new Node<T>;
        Node<T>* slowNode = new Node<T>;
        fastNode = tail->nextNode;
        slowNode = tail->nextNode;
        
        // fastNode will traverse twice as fast as slowNode
        bool moveSlow = false;
        while (fastNode != nullptr) {
            if (moveSlow) slowNode = slowNode->nextNode;
            fastNode = fastNode->nextNode;
            moveSlow = moveSlow ? false : true;
        }
        delete fastNode;
        return slowNode->nodeValue;
    }
    bool HasLoop() {
        Node<T>* fastNode = new Node<T>;
        Node<T>* slowNode = new Node<T>;
        fastNode = tail->nextNode;
        slowNode = tail->nextNode;

        // fastNode will traverse twice as fast as slowNode
        bool moveSlow = false;
        while (fastNode != nullptr) {
            if (fastNode->nextNode == slowNode->nextNode) {
                delete fastNode;
                delete slowNode;
                return true;
            }
            if (moveSlow) slowNode = slowNode->nextNode;
            fastNode = fastNode->nextNode;
            moveSlow = moveSlow ? false : true;
        }
        delete fastNode;
        delete slowNode;
        return false;
    }
};


void printList(std::list<int>* listofInts) {
    std::cout << "listofInts = { ";
    for (int n : *listofInts)
        std::cout << n << ", ";
    std::cout << "};\n";
}

int main()
{
    // Linked Lists using C++ standard library;
    std::list<int> listofInts;
    listofInts.push_back(1);
    listofInts.push_back(2);
    listofInts.push_back(3);
    // Print out the list
    printList(&listofInts);
    listofInts.pop_back();
    printList(&listofInts);
    std::cout << "list size: " << listofInts.size() << std::endl;

    // Linked Lists using hand built library;
    LinkedList<int>* linkedList = new LinkedList<int>();
    linkedList->AddFirst(1);
    linkedList->AddFirst(2);
    linkedList->AddFirst(3);
    linkedList->AddLast(4);
    linkedList->Print();
    linkedList->DeleteFirst();
    linkedList->Print();
    linkedList->DeleteLast();
    linkedList->Print();
    linkedList->DeleteLast();
    linkedList->Print();
    linkedList->DeleteLast();
    linkedList->Print();
    linkedList->DeleteLast();
    linkedList->Print();
    linkedList->DeleteFirst();
    linkedList->Print();
    linkedList->AddLast(4);
    linkedList->Print();
    linkedList->DeleteFirst();
    linkedList->AddFirst(1);
    linkedList->AddFirst(2);
    linkedList->AddFirst(3);
    linkedList->AddLast(4);
    linkedList->Print();
    std::cout << "Contains(4): " << linkedList->Contains(4) << std::endl;
    std::cout << "IndexOf(4): " << linkedList->IndexOf(4) << std::endl;
    std::cout << "Length: " << linkedList->GetSize() << std::endl;
    int* linkedListArray = new int[linkedList->GetSize()];
    linkedList->ToArray(linkedListArray);
    std::cout << "Linked list to array: { ";
    for (int i = 0; i < linkedList->GetSize(); i++)
    {
        std::cout << linkedListArray[i] << ", ";
    }
    std::cout << " }" << std::endl; 

    linkedList->DeleteFirst();
    linkedList->Print();
    linkedList->DeleteLast();
    linkedList->Print();
    linkedList->Reverse();
    linkedList->Print();
    linkedList->AddFirst(3);
    linkedList->Print();
    linkedList->Reverse();
    linkedList->Print();
    std::cout << "GetKthNodeFromEnd(1): " << linkedList->GetKthNodeFromEnd(1) << std::endl;
    std::cout << "FindMiddle: " << linkedList->FindMiddle() << std::endl;
    linkedList->AddFirst(7);
    linkedList->AddFirst(8);
    linkedList->AddFirst(9);
    linkedList->Print();
    std::cout << "FindMiddle: " << linkedList->FindMiddle() << std::endl;
}

