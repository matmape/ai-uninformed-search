using System;
using System.Collections.Generic;

namespace SearchAlgorithms.Models
{
    public class Graph<T> where T :  IComparable
    {
        public List<Edge<T>> Edges = new List<Edge<T>>();
        public List<Node<T>> Nodes = new List<Node<T>>();
        private TreeNode<Node<T>> _adjacencyMatrix;
        private int _nodeCount;
        public Graph(int nodeCount)
        {
            _nodeCount = nodeCount;
            _adjacencyMatrix = new TreeNode<Node<T>>();
             
        }
        public void AddNode(Node<T> node)
        {
            if (node != null) Nodes.Add(node);
        }
        public void AddEdge(Edge<T> edge, int nodeIndex)
        {
            if (edge != null) Edges.Add(edge);

            // _adjacencyMatrix[nodeIndex].Add("1");
        }
        public void AddNodes(List<Node<T>> nodes)
        {
            foreach (var node in nodes)
            {
                AddNode(node);
            }
        }
        public void AddEdges(List<Edge<T>> edges)
        {
            foreach (var edge in edges)
            {
                Edges.Add(edge);
            }
        }
        public void PrintGraph()
        {
            foreach (var item in Nodes)
            {
                Console.WriteLine(item.NodeName);
            }
        }
    }
}