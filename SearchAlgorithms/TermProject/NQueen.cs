using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchAlgorithms.TermProject
{
    public class NQueen
    {
        void Main()
        {
            var n = 13;
            var solution = Solve(n);
            Print(solution);
        }

        ///Solve N-Queens
        public IEnumerable<Tuple<int, int>> Solve(int n)
        {
            return SolveForRemainder(Enumerable.Empty<Tuple<int, int>>(), n);
        }

        ///Prints a chess board with marked queen positions
        public void Print(IEnumerable<Tuple<int, int>> queenLocations, char emptySpot = 'O', char queenSpot = 'x')
        {
            var size = queenLocations.Count();
            if (size == 0) Console.WriteLine("no solution");
            var emptyLine = Enumerable.Range(0, size).Select(_ => emptySpot).ToArray();
            var lines = queenLocations.OrderBy(x => x.Item1).Select(x =>
                new string(emptyLine, 0, x.Item2) + queenSpot + new string(emptyLine, 0, size - x.Item2 - 1));
            Console.WriteLine(string.Join(Environment.NewLine, lines));
        }

        ///Depth-first search of possible queen spaces.
        ///Recursively places queens in legal positions, backtracking when stuck and quitting when the answer is found.
        private IEnumerable<Tuple<int, int>> SolveForRemainder(IEnumerable<Tuple<int, int>> existingPositions, int range)
        {
            if (existingPositions.Count() == range) return existingPositions;
            var potentialNextPositions = NextQueenPossibilities(existingPositions, range);
            var explorations = potentialNextPositions.Select(position =>
                SolveForRemainder(existingPositions.Concat(new[] { position }).ToArray(), range));
            return explorations.FirstOrDefault(x => x.Any()) ?? Enumerable.Empty<Tuple<int, int>>();
        }

        ///Given two lists of numbers, this returns all pairs (the cartesian product).
        private IEnumerable<Tuple<int, int>> AllCombinations(IEnumerable<int> validCols, IEnumerable<int> validRows)
        {
            return validCols.Join(validRows, _ => true, _ => true, (m, n) => Tuple.Create(m, n));
        }

        ///Determines if two points are on a diagonal
        ///ie: returns true if the slope between two points is 1 or -1. false otherwise.
        private bool PositionsAreDiagonal(Tuple<int, int> a, Tuple<int, int> b)
        {
            var xdif = a.Item1 - b.Item1;
            var ydif = a.Item2 - b.Item2;
            return (xdif == ydif || xdif + ydif == 0);
        }

        ///This returns all legal positions for the next queen.
        ///In other words, given a set of existing queen positions,
        ///this returns all points on the board except any rows, columns or diagonals already occupied by queens.
        private IEnumerable<Tuple<int, int>> NextQueenPossibilities(IEnumerable<Tuple<int, int>> queens, int range)
        {
            var validCols = Enumerable.Range(0, range).Except(queens.Select(x => x.Item1));
            var validRows = Enumerable.Range(0, range).Except(queens.Select(x => x.Item2));
            return AllCombinations(validCols, validRows).Where(x => !queens.Any(y => PositionsAreDiagonal(x, y)));
        }
    }
}