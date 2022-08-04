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
    void TraversePreOrder(Node* root)
    {
        if (root == nullptr)
            return;
        std::cout << root->_value << ", ";
        TraversePreOrder(root->_left);
        TraversePreOrder(root->_right);
    }
    void TraverseInOrder(Node* root)
    {
        if (root == nullptr)
            return;
        TraverseInOrder(root->_left);
        std::cout << root->_value << ", ";
        TraverseInOrder(root->_right);
    }
    void TraversePostOrder(Node* root)
    {
        if (root == nullptr)
            return;
        TraversePostOrder(root->_left);
        TraversePostOrder(root->_right);
        std::cout << root->_value << ", ";
    }
    int Height(Node* root)
    {
        if (root == NULL) return 0;
        if (root->_left == nullptr && root->_right == nullptr) return 0;
        return 1 + std::max(Height(root->_left), Height(root->_right));
    }
    bool IsLeaf(Node* node)
    {
        return node->_left == nullptr && node->_right == nullptr;
    }
    int Min(Node* root)
    {
        int left;
        int right;
        if (IsLeaf(root))
            return root->_value;
        else if (root->_left==nullptr)
        {
	        right = Min(root->_right);
            return std::min(right, root->_value);
        }
        else if (root->_right == nullptr)
        {
            left = Min(root->_left);
            return std::min(left, root->_value);
        }
        else
        {
            left = Min(root->_left);
            right = Min(root->_right);
            return std::min(std::min(left, right), root->_value);
        }
        
    }
    int Max(Node* root)
    {

    	if (root->_right != nullptr)
        {
            Max(root->_right);
        }
        else
        {
            return root->_value;
        }

    }
    bool IsEqualTo(Node* root1, Node* root2)
    {
        if (root1 == nullptr && root2 == nullptr) return true;
        if (root1 == nullptr || root2 == nullptr) return false;
        bool LeftEqual = IsEqualTo(root1->_left, root2->_left);
        bool rightEqual = IsEqualTo(root1->_right, root2->_right);
        if (root1->_value == root2->_value && LeftEqual && rightEqual) return true;
        return false;
    }
    bool TreeValidation(Node* root, int leftLimit, int rightLimit)
    {
        bool leftValid = true;
        bool rightValid = true;
        if (root->_left != nullptr) leftValid = TreeValidation(root->_left, leftLimit, root->_value);
        if (root->_right != nullptr)  rightValid = TreeValidation(root->_right, root->_value, rightLimit);
        if (leftValid && rightValid && (root->_value > leftLimit && root->_value < rightLimit)) return true;
        else return false;
    }
    void PrintNodesAtKDepth(Node* root, int remainingDistance)
    {
        if (remainingDistance == 0) {
            std::cout << root->_value << std::endl;
            return;
        }
        if (root->_left != nullptr) PrintNodesAtKDepth(root->_left, remainingDistance - 1);
        if (root->_right != nullptr) PrintNodesAtKDepth(root->_right, remainingDistance - 1);

    }
    int GetTreeSize(Node* root)
    {
        int leftSize = 0;
        int rightSize = 0;
        if(root->_left != nullptr) leftSize = GetTreeSize(root->_left);
        if (root->_right != nullptr) rightSize = GetTreeSize(root->_right);
        return 1 + leftSize + rightSize;
    }
    int CountLeaves(Node* root)
    {
        if (IsLeaf(root)) return 1;
        int leftLeaves = 0;
        int rightLeaves = 0;
        if (root->_left != nullptr) leftLeaves = CountLeaves(root->_left);
        if (root->_right != nullptr) rightLeaves = CountLeaves(root->_right);
        return leftLeaves + rightLeaves;
    }
    bool Contains(Node* root, int k)
    {
        if (root->_value == k) return true;
        else if (root->_value > k && root->_left  != nullptr) Contains(root->_left,  k);
        else if (root->_value < k && root->_right != nullptr) Contains(root->_right, k);
        else return false;
    }
    bool AreSiblings(Node* root, int a, int b)
    {
        if (root->_left->_value == a && root->_right->_value == b) return true;
        if (root->_left != nullptr)  AreSiblings(root->_left,  a, b);
        if (root->_right != nullptr) AreSiblings(root->_right, a, b);
        else return false;
    }
public:
    BinaryTree(int rootValue) {
        root = new Node(rootValue);
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
    bool Find(int value)
    {
        Node* tempNode = root;
        while (tempNode!=nullptr) {
            if (tempNode->_value == value) return true;
            else if (value > tempNode->_value) tempNode = tempNode->_right;
            else if (value < tempNode->_value) tempNode = tempNode->_left;
        }
		return false;
    }
    void TraversePreOrder()
    {
        std::cout << "Tree Depth First Search Pre Order Traversal: " << std::endl;
        TraversePreOrder(root);
        std::cout << std::endl;
    }
    void TraverseInOrder()
    {
        std::cout << "Tree Depth First Search In Order Traversal: " << std::endl;
        TraverseInOrder(root);
        std::cout << std::endl;
    }
    void TraversePostOrder()
    {
        std::cout << "Tree Depth First Search Post Order Traversal: " << std::endl;
        TraversePostOrder(root);
        std::cout << std::endl;
    }
    int Height()
    {
        return Height(root);
    }
    int Min()
    {
        return Min(root);
    }
    int Max()
    {
        return Max(root);
    }
    bool IsEqualTo(BinaryTree tree)
    {
        return IsEqualTo(root, tree.root);
    }
    bool TreeValidation()
    {
        return TreeValidation(root, INT32_MIN, INT32_MAX);
    }
    void InsertBadNode()
    {
        root->_left->_right = new Node(21);
    }
    void PrintNodesAtKDepth(int k)
    {
        PrintNodesAtKDepth(root, k);
    }
    void GetNodesAtDistance(Node* root, int i);
    void TraverseLevelOrder()
    {
        for (int i = 0; i <=Height(); i++)
        {
            std::cout << "Level: " << i << std::endl;
            PrintNodesAtKDepth(root, i);
            
        }
    }
    int GetTreeSize()
    {
        return GetTreeSize(root);
    }
    int CountLeaves()
    {
        return CountLeaves(root);
    }
    bool Contains(int k)
    {
        return Contains(root, k);
    }
    bool AreSiblings(int a, int b)
    {
        return AreSiblings(root, a, b);
    }
};

int main()
{
    // Binary Tree Notes
    /*
     * Depth First Search:
     *  - Pre-Order:    Root -> Left -> Right
     *  - In-Order:     Left -> Root -> Right
     *  - Post-Order:   Left -> Right -> Root
     *  Breadth First Search:
     */
    BinaryTree tree = { 5 };
    tree.Add(15);
    tree.Add(6);
    tree.Add(1);
    tree.Add(8);
    tree.Add(12);
    tree.Add(18);
    tree.Add(17);
    //tree.InsertBadNode();
    std::cout << "Contains(12): " << tree.Contains(12) << std::endl;

    tree.TraversePreOrder();
    tree.TraverseInOrder();
    tree.TraversePostOrder();
    std::cout << "Tree Height: " << tree.Height() << std::endl;
    std::cout << "Tree Min: " << tree.Min() << std::endl;

    BinaryTree tree2 = { 5 };
    tree2.Add(15);
    tree2.Add(6);
    tree2.Add(1);
    tree2.Add(8);
    tree2.Add(12);
    tree2.Add(18);
    tree2.Add(17);
    tree2.Add(19);
    std::cout << "Trees Equal?: " << tree.IsEqualTo(tree2) << std::endl;

    std::cout << "Tree Valid?: " << tree.TreeValidation() << std::endl;

    std::cout << "Print Tree Depth 0: " << std::endl;
    tree.PrintNodesAtKDepth(0);
    std::cout << "Print Tree Depth 1: " << std::endl;
    tree.PrintNodesAtKDepth(1);
    std::cout << "Print Tree Depth 2: " << std::endl;
    tree.PrintNodesAtKDepth(2);
    std::cout << "Print Tree Depth 3: " << std::endl;
    tree.PrintNodesAtKDepth(3);
    std::cout << "Print Tree Depth 4: " << std::endl;
    tree.PrintNodesAtKDepth(4);
    tree.TraverseLevelOrder();
    std::cout << "Tree Size: " << tree.GetTreeSize() << std::endl;
    std::cout << "Tree Leaf Count: " << tree.CountLeaves() << std::endl;
    std::cout << "Tree Max: " << tree.Max() << std::endl;
    std::cout << "Tree Contains 15?: " << tree.Contains(15) << std::endl;
    std::cout << "Tree Contains 20?: " << tree.Contains(20) << std::endl;
    std::cout << "Are {1,15} Siblings?: " << tree.AreSiblings(1,15) << std::endl;
}

