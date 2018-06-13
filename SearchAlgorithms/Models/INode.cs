using System;
using System.Collections.Generic;

namespace SearchAlgorithms.Models
{
    public interface INode<T> where T : IComparable
    {
        int Depth { get; set; }
        Node<T> Parent { get; set; }
        Node<T> LeftChild { get; set; }

        Node<T> RightChild { get; set; }

        string NodeName { get; set; }

        List<Node<T>> List { get; set; }

        double Cost { get; set; }
        List<Node<T>> Children { get; set; }
    }
}