using System;
using System.Dynamic;

namespace AVLTrees
{

    class AVLTree
    {
        #region Private Classes
        private class AVLNode
        {
            public int NodeValue { get; set; }
            public int Height { get; set; }
            public AVLNode? Left { get; set; }
            public AVLNode? Right { get; set; }
            public AVLNode(int value)
            {
                NodeValue = value;
                Left = null;
                Right = null;
            }
        }
        #endregion
        #region Private Fields
        private AVLNode? _root;
        #endregion
        #region Private Methods
        private AVLNode Insert(AVLNode? root, int value)
        {
            if      (value > root.NodeValue && root.Right == null) root.Right = new AVLNode(value);
            else if (value < root.NodeValue && root.Left  == null) root.Left  = new AVLNode(value);
            else if (value > root.NodeValue && root.Right != null) root.Right = Insert(root.Right, value);
            else if (value < root.NodeValue && root.Left  != null) root.Left = Insert(root.Left, value);

            root.Height = setHeight(root);
            return Balance(root);
        }
        private AVLNode Balance(AVLNode root)
        {
            if (isLeftHeavy(root))
            {
                if (BalanceFactor(root.Left) < 0) root.Left = RotateLeft(root.Left);
                return RotateRight(root);
            }
            else if (isRightHeavy(root))
            {
                if (BalanceFactor(root.Right) > 0) root.Right = RotateRight(root.Right);
                return RotateLeft(root);
            }
            return root;
        }
        private AVLNode RotateLeft(AVLNode root)
        {
            /*
             *  10 root
             *      20 newRoot
             *          30
             */
            //AVLNode newRoot = root.Right;
            //root.Right = newRoot.Left;
            //newRoot.Left = root;
            //root.Height = setHeight(root);
            //newRoot.Height = setHeight(newRoot);

            AVLNode newRoot = root.Right;
            root.Right = newRoot.Left;
            newRoot.Left = root;
            root.Height = setHeight(root);
            newRoot.Height = setHeight(newRoot);
            return newRoot;

        }
        private AVLNode RotateRight(AVLNode root)
        {
            /*
             *          10 root
             *      20 newRoot
             *  30
             * reset height of root and new root
             */
            AVLNode newRoot = root.Left;
            root.Left = newRoot.Right;
            newRoot.Right = root;
            root.Height = setHeight(root);
            newRoot.Height = setHeight(newRoot);
            return newRoot;
        }
        private int setHeight(AVLNode root)
        {
            return Math.Max(Height(root.Left), Height(root.Right)) + 1;
        }
        private Boolean isLeftHeavy(AVLNode node)
        {
            return BalanceFactor(node) > 1;
        }
        private Boolean isRightHeavy(AVLNode node)
        {
            return BalanceFactor(node) < -1;
        }
        private int BalanceFactor(AVLNode node)
        {
            return (node ==  null) ? 0 : Height(node.Left) - Height(node.Right);
        }
        private int Height(AVLNode node)
        {
            return (node == null) ? -1 : node.Height;
        }
        #endregion
        #region Public Methods
        public void Insert(int value)
        {
            Console.WriteLine($"Inserting {value}");
            _root ??= new AVLNode(value);
            _root = Insert(_root, value);
        }
        #endregion
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region AVL Tree Notes
            /*
             * Self Balancing Tree Data Types
             * - AVL Trees (Adelson-Velsky and Landis)
             * - Red-black Trees
             * - B-Trees
             * - Splay Trees
             * - 2-3 Trees
             * 
             * AVL Trees automatically rebalance themselves everytime a node is added.
             * This solves the problem of skewed trees in binary search trees.
             * It does this by checking height(left) - height(right) <= 1.
             * 
             * Rotation Types:
             * Left (L)
             * Right (R)
             * Left-Right (LR)
             * Right-Left (RL)
             * 
             * Which type of rotation depends on which side of tree is heavy
             */
            #endregion
            var avlTree = new AVLTree();
            avlTree.Insert(12);
            avlTree.Insert(3);
            avlTree.Insert(9);
            avlTree.Insert(4);
            avlTree.Insert(6);
            avlTree.Insert(2);
            /*
             *          4
             *'       /   \
             *      3       9
             *'   /       /   \ 
             *  2       6       12
             */
            //avlTree.Insert(30);
            //avlTree.Insert(10);
            //avlTree.Insert(20);
            Console.WriteLine("Hello");

        }
    }
}




