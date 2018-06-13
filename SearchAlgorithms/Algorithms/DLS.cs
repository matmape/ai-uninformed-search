using System;
using System.Collections.Generic;
using System.Linq;
using SearchAlgorithms.Models;

namespace SearchAlgorithms.Algorithms
{
    public class DepthLimitedSearch<T> : SearchAlgo<T> where T : IComparable
    {
        Node<T> _goalNode, _startNode;
        private int _limit, depth;
        public DepthLimitedSearch(Node<T> goalNode, Node<T> startNode, int limit) : base(goalNode, startNode)
        {
            _goalNode = goalNode;
            _startNode = startNode;
            _limit = limit;
        }


        //public override void Search()
        //{
        //    var visitedNodes = new Dictionary<Node<T>, int>();
        //    var stack = new Stack<Node<T>>();
        //    var depthStack = new Stack<int>();
        //    depth = 0;
        //    var path = string.Empty;
        //    if (!visitedNodes.Keys.Contains(_startNode))
        //    {
        //        stack.Push(_startNode);
        //        depthStack.Push(depth);
        //        visitedNodes.Add(_startNode, depth);
        //    }

        //    Log($"Start Node : {_startNode.NodeName}");
        //    if (_startNode.NodeName.Equals(_goalNode.NodeName))
        //    {
        //        Log($"Goal Node found");
        //        LogVisitedNodes(visitedNodes);
        //        return;
        //    }
        //    while (stack.Count > 0)
        //    {

        //        var currentNode = stack.Pop();
        //        depth = depthStack.Pop();
        //        if (currentNode.NodeName.Equals(_goalNode.NodeName))
        //        {
        //            Log($"Goal Node found {currentNode.NodeName} Current Depth: {depth}");
        //            path += currentNode.NodeName;
        //            Console.Write($"Path: {path}");
        //            if (!visitedNodes.Keys.Contains(currentNode))
        //                visitedNodes.Add(currentNode, depth);
        //            break;
        //        }
        //        if (visitedNodes.Keys.Contains(currentNode) && depth>0)
        //        {
        //            depth = visitedNodes[currentNode];
        //        }
        //        Log($"Node: {currentNode.NodeName},  Current Depth: {depth}");
        //        if (depth < _limit)
        //        {
        //            var children = currentNode.Children;
        //            if (children.Count > 0)
        //            {
        //                if (visitedNodes.Values.Contains(depth))
        //                    depth++;
        //                foreach (var x in children)
        //                {
        //                    if (x.NodeName.Equals(_goalNode.NodeName))
        //                    {
        //                        if (!visitedNodes.Keys.Contains(x))
        //                            visitedNodes.Add(x, depth);
        //                        path += currentNode.NodeName;
        //                        Console.Write($"Path: {path}"); 
        //                        Log($"\nGoal Node  {x.NodeName} found: Current Depth: {depth}");
        //                        return;
        //                    }
        //                    if (visitedNodes.Keys.Contains(x)) continue;
        //                    visitedNodes.Add(x, depth);
        //                    stack.Push(x);
        //                }                           
        //            }
        //        }
        //        else
        //        {
        //            Log($"Goal node not found at LIMIT {_limit}");
        //            break;
        //        }


        //        path += currentNode.NodeName + " ";

        //    }
        //}
        public override void Search()
        {
            var visitedNodes = new List<Node<T>>();
            var stack = new Stack<Node<T>>();
            var depthStack = new Stack<int>();
            depth = 0;
            stack.Push(_startNode);
            depthStack.Push(depth);
            var path = string.Empty;
            visitedNodes.Add(_startNode);
            Log($"Start Node : {_startNode.NodeName}");
            if (_startNode.NodeName.Equals(_goalNode.NodeName))
            {
                Log($"Goal Node found {_startNode.NodeName} Current Depth: {depth}");
                LogVisitedNodes(visitedNodes);
            }
            else
            {

                while (stack.Count != 0)
                {

                    var currentNode = stack.Pop();
                    depth = depthStack.Pop();
                    if (!visitedNodes.Contains(currentNode))
                    {
                        visitedNodes.Add(currentNode);
                    }
                    if (currentNode.NodeName.Equals(_goalNode.NodeName))
                    {
                        Log($"Goal Node found {currentNode.NodeName} Current Depth: {depth}");
                        path += currentNode.NodeName;
                        Console.Write($"Path: {path}");
                        break;
                    }
                    if (depth < _limit)
                    {
                        var children = currentNode.Children;
                        children.Reverse();
                        foreach (var x in children)
                        {
                            stack.Push(x);
                            depthStack.Push(depth+1);
                            if (!visitedNodes.Contains(x))
                            {
                                visitedNodes.Add(x);
                            }

                        }
                        path += currentNode.NodeName + " ";
                    }
                }

            }

        }
    }
}
