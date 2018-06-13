using System;

namespace SearchAlgorithms.TermProject
{
    public class EightQueensProblem
    {
        private int j;                                                          // Current column.
        private int i;                                                          // x[j]
        private BoundedArray<int> x = new BoundedArray<int>(1, 8);              // Queen position.
        private BoundedArray<bool> a = new BoundedArray<bool>(1, 8, true);      // true -- no queen in row.
        private BoundedArray<bool> b = new BoundedArray<bool>(2, 16, true);     // true -- no queen in /-diagonal.
        private BoundedArray<bool> c = new BoundedArray<bool>(-7, 7, true);     // true -- no queen in \-diagonal.

        public void Solve()
        {
            ConsiderFirstColumn();
            do
            {
                TryColumn();
                if (Safe)
                {
                    SetQueen();
                    ConsiderNextColumn();
                }
                else
                {
                    Regress();
                }
            }
            while (!LastColumnDone && !RegressOutOfFirstColumn);
        }

        public void PrintSolution()
        {
            if (j > 8)
            {
                PrintChessBoard();
            }
            else
            {
                PrintError();
            }
        }

        private void TryColumn()
        {
            do
            {
                AdvancePointer();
            }
            while (!Safe && !LastSquare);
        }

        private void Regress()
        {
            ReconsiderPriorColumn();
            if (!RegressOutOfFirstColumn)
            {
                RemoveQueen();
                if (LastSquare)
                {
                    ReconsiderPriorColumn();
                    if (!RegressOutOfFirstColumn)
                    {
                        RemoveQueen();
                    }
                }
            }
        }

        private void ConsiderFirstColumn()
        {
            j = 1;
            i = 0;
        }

        private void ConsiderNextColumn()
        {
            x[j] = i;
            j++;
            i = 0;
        }

        private void ReconsiderPriorColumn()
        {
            j--;
            i = x[j];
        }

        private void AdvancePointer()
        {
            i++;
        }

        private bool LastSquare
        {
            get { return i == 8; }
        }

        private bool LastColumnDone
        {
            get { return j > 8; }
        }

        private bool RegressOutOfFirstColumn => j < 1;

        private bool Safe => a[i] && b[i + j] && c[i - j];

        private void SetQueen()
        {
            a[i] = b[i + j] = c[i - j] = false;
        }

        private void RemoveQueen()
        {
            a[i] = b[i + j] = c[i - j] = true;
        }

        private void PrintError()
        {
            Console.WriteLine("Solution not found.");
        }

        private void PrintChessBoard()
        {
            for (int row = 1; row <= 8; row++)
            {
                BeginRow();
                for (int column = 1; column <= 8; column++)
                {
                    PrintCell(row, column);
                }
                FinishRow();
            }
            // Print bottom of the chess board.
            BeginRow();
        }

        private void PrintCell(int row, int column)
        {
            if (x[column] == row)
            {
                Console.Write("|x");
            }
            else
            {
                Console.Write("| ");
            }
        }

        private void BeginRow()
        {
            for (int column = 1; column <= 8; column++)
            {
                Console.Write(" -");
            }
            Console.WriteLine();
        }

        private void FinishRow()
        {
            Console.WriteLine("|");
        }
        void Main(string[] args)
        {
            var problem = new EightQueensProblem();
            problem.Solve();
            problem.PrintSolution();
            Console.ReadKey();
        }
    }
}