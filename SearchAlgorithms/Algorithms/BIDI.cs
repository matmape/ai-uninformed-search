using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithms.Models;

namespace SearchAlgorithms.Algorithms
{
    public class BidirectionalSearch<T> : SearchAlgo<T> where T : IComparable
    {
        Node<T> _goalNode, _startNode;
        public BidirectionalSearch(Node<T> goalNode, Node<T> startNode) : base(goalNode, startNode)
        {
            _goalNode = goalNode;
            _startNode = startNode;
        }

        public override void Search()
        {
            var visitedNodesFromRootNode = new List<Node<T>>();
            var visitedNodesFromGoalNode = new List<Node<T>>();
            var queue1 = new Queue<Node<T>>();
            var queue2 = new Queue<Node<T>>();

            Log($"Start Node : {_startNode.NodeName}");
            if (_startNode.NodeName.Equals(_goalNode.NodeName))
            {
                Log($"Goal Node found");
                LogVisitedNodes(visitedNodesFromRootNode);
            }
            else
            {
                if (!visitedNodesFromRootNode.Contains(_startNode))
                {
                    queue1.Enqueue(_startNode);
                    //root node
                    queue2.Enqueue(_goalNode);
                    visitedNodesFromRootNode.Add(_startNode);
                }
                while (queue1.Count > 0 && queue2.Count>0)
                {
                    var currentNode = queue1.Dequeue();

                    if (currentNode.NodeName.Equals(_goalNode.NodeName))
                    {
                        Log($"Goal Node found {currentNode.NodeName}");
                        if (!visitedNodesFromRootNode.Contains(currentNode))
                            visitedNodesFromRootNode.Add(currentNode);
                        break;
                    }
                    var children = currentNode.Children;
                    //var children = currentNode.Children();
                    if (children == null || children.Count <= 0) continue;
                    foreach (var child in children)
                    {
                        if (child.NodeName.Equals(_goalNode.NodeName))
                        {

                            if (!visitedNodesFromRootNode.Contains(child))
                                visitedNodesFromRootNode.Add(child);
                            LogVisitedNodes(visitedNodesFromRootNode);
                            Log($"\nGoal Node found: {child.NodeName}");
                            break;
                        }
                        if (visitedNodesFromRootNode.Contains(child)) continue;
                        visitedNodesFromRootNode.Add(child);
                        queue1.Enqueue(child);
                    }
                    //Goal Node BFS
                    //Searching from Goal Node
                    var currentNode1 = queue2.Dequeue();

                    if (currentNode1.NodeName.Equals(_startNode.NodeName))
                    {
                        Log($"Goal Node found {currentNode1.NodeName}");
                        if (!visitedNodesFromGoalNode.Contains(currentNode1))
                            visitedNodesFromGoalNode.Add(currentNode1);
                        break;
                    }
                    var children1 = currentNode1.Children;
                    //var children1 = currentNode1.Children();
                    if (children1 == null || children1.Count <= 0) continue;
                    foreach (var child in children1)
                    {
                        if (child.NodeName.Equals(_startNode.NodeName))
                        {

                            if (!visitedNodesFromGoalNode.Contains(child))
                                visitedNodesFromGoalNode.Add(child);
                            LogVisitedNodes(visitedNodesFromGoalNode);
                            Log($"\nGoal Node found: {child.NodeName}");
                            break;
                        }
                        if (visitedNodesFromGoalNode.Contains(child)) continue;
                        visitedNodesFromGoalNode.Add(child);
                        queue2.Enqueue(child);
                    }
                }

            }

        }
    }
}
