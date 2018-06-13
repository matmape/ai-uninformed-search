namespace SearchAlgorithms.TermProject
{
    public class NQueens
    {

        public NQueens(int boardConfiguration)
        {
            _board = new int[boardConfiguration];
            _maxRows = _maxCols = boardConfiguration;
        }

        private static int[] _board;
        private static int _maxRows, _maxCols;
        public int[] GetPosition()
        {
            return GetPosition(0) ? _board : null;
        }
        public bool IsColliding(int row, int col)
        {
            for (var i = 0; i < col; i++)
            {
                if (_board[i] == row) return true; // Same Row
                if ((_board[i] + col - i == row) || (_board[i] - col + i == row))
                    return true;
            }
            return false;

        }
        public bool GetPosition(int col)
        {
            if (col == _maxCols) return true;
            for (var row = 0; row < _maxRows; row++)
                if (!IsColliding(row, col))
                {
                    _board[col] = row;
                    if (GetPosition(col + 1)) return true;
                }
            return false; 
        }
    }
}