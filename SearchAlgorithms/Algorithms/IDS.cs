using System;
using System.Collections.Generic;
using SearchAlgorithms.Models;

namespace SearchAlgorithms.Algorithms
{
    public class IterativeDeepingSearch<T> : SearchAlgo<T> where T :  IComparable
    {
        Node<T> _goalNode, _startNode;
        private int _limit;
        public IterativeDeepingSearch(Node<T> goalNode, Node<T> startNode) : base(goalNode, startNode)
        {
            _goalNode = goalNode;
            _startNode = startNode; 
        }
        public void Search1(int depth)
        {
            var visitedNodes = new List<Node<T>>();
            var stack = new Stack<Node<T>>();
            var depthStack = new Stack<int>(); 
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
                            depthStack.Push(depth + 1);
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
        public int DepthLimitedSearch(int limit, ref bool cutOff)
        {
            var currentNode = _startNode;
            var result = 0;
            var visitedNodes = new List<Node<T>>();
            var stack = new Stack<Node<T>>();
            var depthStack = new Stack<int>();
            stack.Push(_startNode);
            depthStack.Push(0);
            var path = string.Empty;
            while (stack.Count > 0)
            {
                currentNode = stack.Pop();
                if (!visitedNodes.Contains(currentNode))
                    visitedNodes.Add(currentNode);

                var depth = depthStack.Pop();
                Log($"\nDepth: {depth},  Node:{currentNode.NodeName}");
                if (currentNode.NodeName.Equals(_goalNode.NodeName))
                {
                    result = 1;
                    cutOff = true;
                    //LogVisitedNodes(visitedNodes);
                    Log($"\nGoal Node found: {currentNode.NodeName} at depth: {depth+1}");
                    path += currentNode.NodeName;
                    Console.Write($"Path: {path}");
                    break;
                }
                if (depth < limit)
                {
                    var children = currentNode.Children;
                    if (children.Count > 0)
                    {
                        foreach (var x in children)
                        {
                            if (x.NodeName.Equals(_goalNode.NodeName))
                            {
                                cutOff = true;
                                if (!visitedNodes.Contains(x))
                                    visitedNodes.Add(x);
                                //LogVisitedNodes(visitedNodes);
                                Log($"\nGoal Node found: {x.NodeName} at depth: {depth+1}");
                                path += currentNode.NodeName;
                                Console.Write($"Path: {path}");
                                result = 1;
                                return result;
                            }
                            if (visitedNodes.Contains(x)) continue;
                            visitedNodes.Add(x);
                            stack.Push(x);
                            depthStack.Push(depth + 1);
                        }
                    }
                }
                path += currentNode.NodeName + " ";
            }
            path += currentNode.NodeName;
            Console.Write($"Path: {path}");
            return result;

        }
        //public override void Search()
        //{
        //    var cutOff = false;
        //    var depth=0;
        //    while (!cutOff)
        //    {
        //        var status = DepthLimitedSearch(depth,ref cutOff);
        //        if (status == 1)
        //            break;
        //        depth++;
        //    }           
        //}
        public override void Search()
        {
            var cutOff = false;
            var depth = 1;
            while (!cutOff)
            {
                var status = DepthLimitedSearch(depth, ref cutOff);
                if (status == 1)
                    break;
                depth++;
            }
        }
    }
}
