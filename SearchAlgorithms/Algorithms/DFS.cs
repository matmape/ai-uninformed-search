using System;
using System.Collections.Generic;
using SearchAlgorithms.Models;

namespace SearchAlgorithms.Algorithms
{
    public class DepthFirstSearch<T> : SearchAlgo<T> where T : IComparable
    {
        Node<T> _goalNode, _startNode;
        public DepthFirstSearch(Node<T> goalNode, Node<T> startNode) : base(goalNode, startNode)
        {
            _goalNode = goalNode;
            _startNode = startNode;
        }


        public override void Search()
        {
            var visitedNodes = new List<Node<T>>();
            var stack = new Stack<Node<T>>();
            stack.Push(_startNode);
            var path = string.Empty;
            visitedNodes.Add(_startNode);
            Log($"Start Node : {_startNode.NodeName}");
            if (_startNode.NodeName.Equals(_goalNode.NodeName))
            {
                Log($"Goal Node found");
                LogVisitedNodes(visitedNodes);
            }
            else
            {
               
                while (stack.Count !=0)
                {
                     
                    var currentNode = stack.Pop();
                    
                    if (!visitedNodes.Contains(currentNode))
                    {
                        visitedNodes.Add(currentNode);
                    }
                    if (currentNode.NodeName.Equals(_goalNode.NodeName))
                    {
                        Log($"\nGoal Node found");
                        path += currentNode.NodeName;
                        Console.Write($"Path: {path}"); 
                        break;
                    }
                    var children = currentNode.Children;
                    children.Reverse();
                    foreach (var x in children)
                    {
                        stack.Push(x);
                            
                        if (!visitedNodes.Contains(x))
                        {
                            visitedNodes.Add(x); 
                        }
                            
                    }
                    path += currentNode.NodeName+" ";
                }

            }

        }
    }
}
