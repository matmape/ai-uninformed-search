using SearchAlgorithms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithms.Algorithms;
using SearchAlgorithms.TermProject;

namespace SearchAlgorithms
{
    class Program
    {
        private static List<Node<string>> Nodes { get; set; }
         static void Main(string[] args)
        {
            try
            {
                Nodes = new List<Node<string>>();
                UninformedAlgorithms();
                // AStar();
                //               var n=new NQueen();
                //                var solution = n.Solve(4);
                //                n.Print(solution);

                // var s = new AStarNQueen(8);
                //                var problem = new EightQueensProblem();
                //                problem.Solve();
                //                problem.PrintSolution();

              //  NeuralNetwork nn = new NeuralNetwork(21.5, new int[] { 2, 4, 2 });
                Console.ReadKey();
            }
            catch (FormatException ec)
            {
                Console.WriteLine("Wrong input!");
                ShowOptions();
                Console.ReadKey();
            }
            catch (Exception ec)
            {

                throw;
            }
        }

        public static void UninformedAlgorithms()
        {
            Console.WriteLine("Welcome\nARTIFICIAL INTELLIGENCE SEARCH ALGORITHMS");
            ShowOptions();

            var input = Console.ReadLine();
            int res = 1;
            while (int.TryParse(input, out res) == false)
            {
                ShowOptions();
                input = Console.ReadLine();
            }
            Node<string> startNode = null;
            Node<string> goalNode = null;
            switch (Convert.ToInt32(input))
            {
                case 1:
                {
                    startNode = GetRootNode();
                    goalNode = GetGoalNode(); 
                    Console.WriteLine("BREADTH FIRST SEARCH IMPLEMENTATION------------");
                    var bfs = new BreadthFirstSearch<string>(goalNode, startNode);
                    bfs.Search();
                }
                    break;
                case 2:
                {
                    startNode = GetRootNode();
                    goalNode = GetGoalNode(); 
                        Console.WriteLine("DEPTH FIRST SEARCH IMPLEMENTATION------------");
                    var dfs = new DepthFirstSearch<string>(goalNode, startNode);
                    dfs.Search();
                }
                    break;
                case 3:
                {
                    startNode = GetRootNode();
                    goalNode = GetGoalNode(); 
                        Console.WriteLine("\nEnd\n");
                    Console.WriteLine("ITERATIVE DEEPENING SEARCH IMPLEMENTATION------------");
                    var ids = new IterativeDeepingSearch<string>(goalNode, startNode);
                    ids.Search();
                }
                    break;
                case 4:
                {
                    startNode = GetRootNode();
                    goalNode = GetGoalNode(); 
                        Console.WriteLine("DEPTH LIMITED SEARCH IMPLEMENTATION------------");
                    Console.Write("Enter limit to search: ");
                    var limit = int.Parse(Console.ReadLine());
                    var dls = new DepthLimitedSearch<string>(goalNode, startNode, limit);
                    dls.Search();
                }
                    break;
                case 5:
                {
                    startNode = GetRootNode();
                    goalNode = GetGoalNode();
                    
                    Console.WriteLine("Enter the cost for the nodes");
                    foreach (var node in Nodes)
                    {
                        Console.Write($"Enter cost for node: {node.NodeName}: ");
                        node.Cost = double.Parse(Console.ReadLine());

                    }
                    Console.WriteLine("UNIFORM COST SEARCH IMPLEMENTATION------------");
                     
                    var ucs = new UniformCostSearch<string>(goalNode, startNode);
                    ucs.Search();
                }
                    break;
                case 6:
                {
                    startNode = GetRootNode();
                    goalNode = GetGoalNode();
                    Console.WriteLine("BIDIRECTIONAL SEARCH IMPLEMENTATION------------");
                    var bidi = new BidirectionalSearch<string>(goalNode, startNode);
                    bidi.Search();
                }
                    break;
                default:
                    ShowOptions();
                    break;
            }
        }
        public static void AStar()
        {
            var grid = new SquareGrid(10, 10);
            for (var x = 1; x < 4; x++)
            {
                for (var y = 7; y < 9; y++)
                {
                    grid.Walls.Add(new Location(x, y));
                }
            }
            grid.Forests = new HashSet<Location>
            {
                new Location(3, 4), new Location(3, 5),
                new Location(4, 1), new Location(4, 2),
                new Location(4, 3), new Location(4, 4),
                new Location(4, 5), new Location(4, 6),
                new Location(4, 7), new Location(4, 8),
                new Location(5, 1), new Location(5, 2),
                new Location(5, 3), new Location(5, 4),
                new Location(5, 5), new Location(5, 6),
                new Location(5, 7), new Location(5, 8),
                new Location(6, 2), new Location(6, 3),
                new Location(6, 4), new Location(6, 5),
                new Location(6, 6), new Location(6, 7),
                new Location(7, 3), new Location(7, 4),
                new Location(7, 5)
            };

            // Run A*
            var astar = new AStarSearch(grid, new Location(1, 4),new Location(8, 5));

            DrawGrid(grid, astar);
        }
        static void DrawGrid(SquareGrid grid, AStarSearch astar)
        {
            // Print out the cameFrom array
            for (var y = 0; y < 10; y++)
            {
                for (var x = 0; x < 10; x++)
                {
                    Location id = new Location(x, y);
                    Location ptr = id;
                    if (!astar.CameFrom.TryGetValue(id, out ptr))
                    {
                        ptr = id;
                    }
                    if (grid.Walls.Contains(id)) { Console.Write("##"); }
                    else if (ptr.X == x + 1) { Console.Write("\u2192 "); }
                    else if (ptr.X == x - 1) { Console.Write("\u2190 "); }
                    else if (ptr.Y == y + 1) { Console.Write("\u2193 "); }
                    else if (ptr.Y == y - 1) { Console.Write("\u2191 "); }
                    else { Console.Write("* "); }
                }
                Console.WriteLine();
            }
        }
        private static Node<string> GetGoalNode()
        {
            Console.Write("Enter Goal Node: ");
            var consoleInput = Console.ReadLine();
            var goalNode = new Node<string>(consoleInput);
            return goalNode;
        }

        private static Node<string> GetRootNode()
        {
            Console.WriteLine("Enter Root Node");
            var startNode = new Node<string>(Console.ReadLine());
            Nodes.Add(startNode);
            GetInput(startNode); 

            return startNode;
        }

        private static void GetInput(Node<string> node)
        {
            Console.WriteLine($"Enter children for {node.NodeName} -1 for leaf");

            var input = Console.ReadLine();

            if (input.Equals("-1"))
                return;
            else
            {
                foreach (char t in input)
                {
                    var newNode = new Node<string>(t.ToString());     
                    GetInput(newNode);
                    node.Children.Add(newNode);
                    if(!Nodes.Any(c=>c.NodeName.Equals(newNode.NodeName)))
                        Nodes.Add(newNode);//get a collection of the node
                }
            }
            
        }
      
        private static void ShowOptions()
        {
            Console.WriteLine("\nSelect an algorithm to run and q to quit\n");
            Console.WriteLine("1 - Breadth First Search\n");
            Console.WriteLine("2 - Depth First Search\n");
            Console.WriteLine("3 - Iterative Deepening Search\n");
            Console.WriteLine("4 - Depth Limited Search\n");
            Console.WriteLine("5 - Uniform Cost Search\n");
            Console.WriteLine("6 - BiDirectional Search\n");
            Console.Write("Enter option: ");
        }

    }
     
}

