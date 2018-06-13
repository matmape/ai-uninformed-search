using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms.Models
{
    public class Node<T> : INode<T> where T : IComparable
    {
        private T _data;
        public int Depth { get; set; }
        public Node<T> Parent { get; set;}
        public Node<T> LeftChild { get; set; }

        public Node<T> RightChild { get; set; }

        public string NodeName { get; set; }

        public List<Node<T>> List { get;  set; }

        public double Cost { get; set; }
        public List<Node<T>> Children { get; set; }
         
        public Node(string nodeName, Node<T> parent=null) : this(nodeName)
        { 
            Parent = parent;
            if (Parent == null)
                Depth = 0;
            else
                Depth = Parent.Depth + 1;
        } 
        public Node(string nodeName)
        {
            NodeName = nodeName; 
            LeftChild = null;
            NodeName = nodeName;
            RightChild = null;
            Parent = null;
            Children = new List<Node<T>>();
        }
        public Node(string nodeName, double cost) :this(nodeName)
        {
            Cost = cost;
        } 
    }
}
