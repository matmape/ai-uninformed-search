using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SearchAlgorithms.TermProject
{
    public class AStarNQueen
    {
        private static int[] _board;
        public AStarNQueen(int boardConfiguration)
        {
            var squaredBoard = (int) Math.Pow(boardConfiguration, 2);
                _board =new int[squaredBoard];
            InitializeBoardWithRandomNumbers(squaredBoard);
        }

        private void InitializeBoardWithRandomNumbers(int boardConfiguration)
        {
            var randomGenerator = new Random();
            var allNumbers = new int[boardConfiguration];

            for (int i = 0; i < boardConfiguration; i++)
            {
                allNumbers[i] = i;  
            }
            ShuffleNumbers(allNumbers, randomGenerator);
            var board = new int[(int) Math.Sqrt(boardConfiguration)];
            Array.Copy(allNumbers,0,board,0,board.Length); 
            Array.Sort(board);
            Console.WriteLine("Randomly generated board");
            PrintBoard(_board);
            var root = new AStarNode(board);

            Console.WriteLine("Starting A* Search...");
            AStar(root);
        }

        private void AStar(AStarNode root)
        {
            var time = DateTime.Now.Millisecond;
            var openList  = new List<AStarNode>();
            var closedList  = new List<AStarNode>();

            openList.Add(root);
            var goalFound = false;
            AStarNode best = null;
            var boardConfiguration = 0;

            while (openList.Count>0)
            {
                var bestValue = int.MinValue;
                var bestHeuristic = -1;
                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].Function > bestValue)
                    {
                        bestValue = openList[i].Function;
                        bestHeuristic = i;
                    }
                }
                best = openList[bestHeuristic];
                openList.RemoveAt(bestHeuristic);
                closedList.Add(best);
                boardConfiguration++;

                if (best.IsGoal())
                {
                    goalFound = true;
                    break;
                }
                else
                {
                    var childrenOfBest = best.Children();
                    if (childrenOfBest == null)
                    {
                        Console.WriteLine("No children");
                        Console.WriteLine(best.IsGoal());
                        return;
                    }
                    foreach (var node in childrenOfBest)
                    {
                        if (node == null)
                        {
                            continue;
                        }
                        if (node.Function >= best.Function)
                        {
                            openList.Add(node);
                        }
                    }
                }
            }
            if (goalFound)
            {
                var time2 = DateTime.Now.Millisecond;
                Console.WriteLine("Goal Board Found");
                PrintBoard(best.Board);
                Console.WriteLine($"{boardConfiguration} were tested");
                Console.WriteLine($"{openList.Count + closedList.Count} were created");
                Console.WriteLine($"Search took approximately {time2 - time} milliseconds");
            }
            else
            {
                Console.WriteLine("Goal board not found");
            }
        }

        private void PrintBoard(int[] board)
        {
            var completedBoard = new int[board.Length];
            for (int i = 0; i < completedBoard.Length; i++)
            {
                completedBoard[board[i]] = 1;
            }
            Console.Write("+");
            for (int i = 0; i < (board.Length * 2 - 1); i++)
            {
                Console.Write("-");
            }
            Console.Write("+");

            for (int i = 0; i < completedBoard.Length; i++)
            {
                Console.Write("|");
                if (i != 0 && i % board.Length == 0)
                {
                    Console.WriteLine();
                    Console.Write("|");
                }
                if (completedBoard[i] == 1)
                {
                    Console.Write("Q");
                }
                else
                {
                    Console.Write("");
                }
            }
            Console.WriteLine("|");
            Console.Write("+");
            for (int i = 0; i < (board.Length * 2 - 1); i++)
            {
                Console.Write("-");
            }
            Console.Write("+");
        }

        private void ShuffleNumbers(int[] board, Random randomGenerator)
        {
            for (var index =board.Length;index>1;index--)
            {
                var j = randomGenerator.Next(index);
                var temp = board[j];
                board[j] = board[index - 1];
                board[index - 1] = temp;
            }
        }
    }

    public class AStarNode : IComparable
    {
        public int CompareTo(object obj)
        {
            var localObj = (AStarNode) obj;
            if (Function < localObj.Function) return -1;
            if (Function > localObj.Function) return 1;
            return 0;
        }

        public bool IsGoal()
        {
            foreach (var queen in worstQueens)
            {
                if (queen != 0) return false;
            }
            return true;
        }
        public int[] Board => _board;
        private AStarNode _parent;
        private int[] _board;
        private int[] worstQueens;
        private int heuristic;

        public AStarNode()
        {
            _parent = null;
        }

        public AStarNode(int[] board)
        {
                _board = new int[board.Length];
            Array.Copy(board, 0, _board, 0, _board.Length);
            heuristic = GenerateHeuristic();
        }

        public AStarNode(AStarNode parent, int[] board):this(board)
        {
            _parent = parent;
        }
        public AStarNode Parent { get; set; }
        public int Heuristic { get; set; }
        public int Function => heuristic;

        public AStarNode[] Children()
        {
            var badQueens = GetWorstQueens();
            var totalChildren = new AStarNode[((_board.Length*_board.Length)-_board.Length)*badQueens.Length];
            var counter = 0;
            var tempBoard = new int[_board.Length];
            Array.Copy(_board, 0, tempBoard, 0, tempBoard.Length);
            
            foreach (var queen in badQueens)
            {
                for (int i = 0; i < (_board.Length*_board.Length); i++)
                {
                    Array.Copy(_board, 0, tempBoard, 0, tempBoard.Length);
                    if (_board.Contains(i) || i == queen)
                    {
                        continue;
                    }
                    else
                    {
                        tempBoard[queen] = i;
                        tempBoard.ToList().Sort();
                        foreach (var j in tempBoard)
                        {
                            Console.Write(j+" "); 
                        }
                        Console.WriteLine();
                        totalChildren[counter++] = new AStarNode(this,tempBoard);
                    }
                }
            }
            return totalChildren;
        }

        private int[] GetWorstQueens()
        {
            var queen = -1;
            var numberOfQueens = 0;
            for (int i = 0; i < worstQueens.Length; i++)
            {
                if (worstQueens[i] >= queen)
                    queen = worstQueens[i];
            }

            for (int i = 0; i < worstQueens.Length; i++)
            {
                if (worstQueens[i] == queen)
                    numberOfQueens++;
            }
            var badQueens = new int[numberOfQueens];
            var temp = 0;

            for (int i = 0; i < worstQueens.Length; i++)
            {
                if (worstQueens[i] == queen)
                    badQueens[temp++] = i;
            }
            return badQueens;
        }

        private int GenerateHeuristic()
        {
            var boardSize = _board.Length;
            var attackCounter = 0;
            var list = new List<int>();
            var hitCount = new int[_board.Length];

            for (int column = 0; column < boardSize; column+=boardSize)
            {
                for (int rowIndex = 0; rowIndex < boardSize; rowIndex++)
                {
                    var queen = column + rowIndex;
                    if (_board.Contains(queen))
                        list.Add(queen);
                }
                if (list.Count > 1)
                {
                    while (list.Count > 0)
                    {
                        var removed = list.First();
                        list.RemoveAt(removed);
                        var temp = Array.BinarySearch(_board, removed);
                        hitCount[temp]++;
                    }
                    attackCounter++;
                }
                list.Clear();
            }
            //searching vertically...
            for (int row = 0; row < boardSize; row++)
            {
                for (int column = 0; column < boardSize; column+=boardSize)
                {
                    var queen = column + row;
                    if (_board.Contains(queen))
                        list.Add(queen);
                }
                if (list.Count > 1)
                {
                    while (list.Count > 0)
                    {
                        var removed = list.First();
                        list.RemoveAt(removed);
                        var temp = Array.BinarySearch(_board, removed);
                        hitCount[temp]++;
                    }
                    attackCounter++;
                }
                list.Clear();
            }
            //Search diagonally
            for (int vertical = 0; vertical < (boardSize+1); vertical++)
            {
                for (int horizontal = 0; horizontal < vertical; horizontal++)
                {
                    var queen = ((boardSize - vertical) * boardSize) + (horizontal) * (boardSize + 1);
                    if (_board.Contains(queen))
                        list.Add(queen);
                }
                if (list.Count > 1)
                {
                    while (list.Count > 0)
                    {
                        var removed = list.First();
                        list.Remove(removed);
                        var temp = Array.BinarySearch(_board, removed);
                        hitCount[temp]++;
                    }
                    attackCounter++;
                }
                list.Clear();
            }
            for (int horizontal = 0; horizontal < boardSize; horizontal++)
            {
                for (int vertical = 0; vertical < (boardSize-horizontal); vertical++)
                {
                    var queen = horizontal+(vertical)*(boardSize+1);
                    if (_board.Contains(queen))
                        list.Add(queen);
                }
                if (list.Count > 1)
                {
                    while (list.Count > 0)
                    {
                        var removed = list.First();
                        list.Remove(removed);
                        var temp = Array.BinarySearch(_board, removed);
                        hitCount[temp]++;
                    }
                    attackCounter++;
                }
                list.Clear();
            }

            //Search up diagonally
            for (int horizontal = 0; horizontal < boardSize; horizontal++)
            {
                for (int vertical = 0; vertical < (horizontal+1); vertical++)
                {
                    var queen = horizontal + (vertical) * (boardSize - 1);
                    if (_board.Contains(queen))
                        list.Add(queen);
                }
                if (list.Count > 1)
                {
                    while (list.Count > 0)
                    {
                        var removed = list.First();
                        list.Remove(removed);
                        var temp = Array.BinarySearch(_board, removed);
                        hitCount[temp]++;
                    }
                    attackCounter++;
                }
                list.Clear();
            }
            for (int horizontal = 2; horizontal < (boardSize+1); horizontal++)
            {
                for (int vertical = 0; vertical < ((boardSize+1)-horizontal); vertical++)
                {
                    var queen = horizontal + (vertical) * (boardSize - 1);
                    if (_board.Contains(queen))
                        list.Add(queen);
                }
                if (list.Count > 1)
                {
                    while (list.Count > 0)
                    {
                        var removed = list.First();
                        list.Remove(removed);
                        var temp = Array.BinarySearch(_board, removed);
                        hitCount[temp]++;
                    }
                    attackCounter++;
                }
                list.Clear();
            }
            worstQueens = hitCount;
            int heuristicValue = boardSize;
            foreach (var i in worstQueens)
            {
                if (i != 0) heuristicValue--;
            }
            return heuristicValue;
        }

        public string PrintBoard()
        {
            var boardString = "{";
            for (int i = 0; i < _board.Length; i++)
            {
                if (i == _board.Length - 1)
                    boardString += _board[i] + "}";
                else
                {
                    boardString += _board[i] + ",";
                }
            }
            return boardString;
        }
//        private bool BoardContains(int[] list, int item)
//        {
//            for (int i = 0; i < list.Length; i++)
//            {
//                if (list[i] == item) return true;
//            }
//            return false;
//        }
       
    }
}
