using System;
using System.Collections.Generic;
using SearchAlgorithms.Models;

namespace SearchAlgorithms.TermProject
{
    public class AStarSearch
    {
        public Dictionary<Location, Location> CameFrom = new Dictionary<Location, Location>();
        public Dictionary<Location, double> CostSoFar = new Dictionary<Location, double>();

        // Note: a generic version of A* would abstract over Location and
        // also Heuristic
        public double Heuristic(Location a, Location b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        public AStarSearch(IWeightedGraph<Location> graph, Location start, Location goal)
        {
            var frontier = new PriorityQueue<Location>();
            frontier.Enqueue(start, 0);

            CameFrom[start] = start;
            CostSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.Equals(goal))
                {
                    break;
                }

                foreach (var next in graph.Neighbors(current))
                {
                    var newCost = CostSoFar[current] + graph.Cost(current, next);
                    if (!CostSoFar.ContainsKey(next)
                        || newCost < CostSoFar[next])
                    {
                        CostSoFar[next] = newCost;
                        var priority = newCost + Heuristic(next, goal);
                        frontier.Enqueue(next, priority);
                        CameFrom[next] = current;
                    }
                }
            }
        }


    }
}