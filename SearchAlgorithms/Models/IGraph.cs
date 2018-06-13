using System;
using System.Collections.Generic;

namespace SearchAlgorithms.Models
{
    public interface IGraph<T> where T: IComparable
    {
        void AddEdge(Edge<T> edge);
        void AddEdges(List<Edge<T>> edges);
        void AddNode(Node<T> node);
        void AddNodes(List<Node<T>> nodes);
    }
}