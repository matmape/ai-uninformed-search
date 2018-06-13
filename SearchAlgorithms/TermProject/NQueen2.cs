using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithms.TermProject
{
    public class NQueen2
    {

        public void Main(string[] args)
        {


            DisplayPositions(4);

        }

        private static void DisplayPositions(int boardSize)
        {
            if (boardSize <= 0) return;
            var positions = new List<Position>();
            if (GetPositions(positions, boardSize, 0, 0))
            {
                Console.WriteLine(string.Join(",", positions));
            }
            else
            {
                Console.WriteLine("Nothing to display");
            }

        }

        private static bool GetPositions(List<Position> positions, int boardSize, int row, int column)
        {
            if (positions.Count == boardSize)
            {
                return true;
            }
            else
            {
                var rowIndex = row;
                var columnIndex = column;

                while (rowIndex < boardSize)
                {
                    if (ValidPosition(positions, rowIndex, columnIndex))
                    {
                        positions.Add(new Position(rowIndex, columnIndex));

                        if (GetPositions(positions, boardSize, rowIndex + 1, 0))
                        {
                            return true;
                        }

                        positions.RemoveAt(positions.Count - 1);
                    }
                    columnIndex++;
                    if (columnIndex == boardSize)
                    {
                        columnIndex = 0;
                        rowIndex++;
                    }
                }
                return false;
            }
        }

        private static bool ValidPosition(List<Position> positions, int rowIndex, int columnIndex)
        {
            foreach (var position in positions)
            {
                if (position.Row == rowIndex || position.Column == columnIndex) return false;
                if (Math.Abs(position.Row - rowIndex) == Math.Abs(position.Column - columnIndex))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class Position
    {
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
        public int Row { get; set; }
        public int Column { get; set; }

        public override string ToString()
        {
            return "R:" + Row + "-C:" + Column;
        }
    }
}
