#include <iostream>

class BinaryTree
{
private:
    struct Node
    {
        Node* _left;
        Node* _right;
        int _value;
        Node(const int value) : _left(nullptr), _right(nullptr), _value(value) {}
    };
    Node* root;
    int _size;
public:
    BinaryTree(int rootValue) : _size(1) {
        root = new Node(rootValue);
    }
    int GetSize()
    {
        return _size;
    }
    void Add(int value)
    {
        Node* tempNode = root;
        while(true) {
            if (value > tempNode->_value) {
                if (tempNode->_right == nullptr) {
                    tempNode->_right = new Node(value);
                    break;
                }
                else tempNode = tempNode->_right;
            }
            else if (value < tempNode->_value) {
                if (tempNode->_left == nullptr) {
                    tempNode->_left = new Node(value);
                    break;
                }
                else tempNode = tempNode->_left;
            }
        }
    }
    bool Contains(int value)
    {
        Node* tempNode = root;
        while (tempNode!=nullptr) {
            if (tempNode->_value == value) return true;
            else if (value > tempNode->_value) tempNode = tempNode->_right;
            else if (value < tempNode->_value) tempNode = tempNode->_left;
        }
		return false;
    }
};

int main()
{
    BinaryTree tree = { 10 };
    tree.Add(5);
    tree.Add(15);
    tree.Add(6);
    tree.Add(1);
    tree.Add(8);
    tree.Add(12);
    tree.Add(18);
    tree.Add(17);
    std::cout << "Contains(12): " << tree.Contains(12) << std::endl;

    std::cout << std::cin.get();
}
