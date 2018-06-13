using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms.Models
{
    public abstract class SearchAlgo<T> where T : IComparable
    {
        Node<T> _goalNode, _startNode;

        protected SearchAlgo(Node<T> goalNode, Node<T> startNode)
        {
            _goalNode = goalNode;
            _startNode = startNode;
        }
        public abstract void Search();

        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("============================================================");
        }
        public void LogVisitedNodes(List<Node<T>> visitedNodes)
        {
            //print visited Nodes
            Console.Write("Path: ");
            visitedNodes.ForEach(x =>
            {
                Console.Write($"{x.NodeName} ");
            });
        }
        public void LogVisitedNodes(Dictionary<Node<T>, int> visitedNodes)
        {
            //print visited Nodes
            visitedNodes.ToList().ForEach(x =>
            {
                Console.Write($"{x.Key.NodeName},  ");
            });
        }
    }
}
