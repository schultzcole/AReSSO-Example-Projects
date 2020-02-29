using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace TicTacToe.State
{
    public class BoardState : IEquatable<BoardState>
    {
        public const int WIDTH = 3;
        public const int HEIGHT = 3;

        public PlayerTag[][] Board { get; }

        public BoardState()
        {
            Board = new PlayerTag[HEIGHT][];
            for (int row = 0; row < HEIGHT; row++)
            {
                Board[row] = new PlayerTag[WIDTH];
            }
        }

        private BoardState(PlayerTag[][] board)
        {
            Board = board;
        }

        [Pure]
        public PlayerTag[] GetRow(int row) => (PlayerTag[])Board[row].Clone();

        [Pure]
        public PlayerTag[] GetCol(int col) => Enumerable.Range(0, HEIGHT).Select(row => Board[row][col]).ToArray();

        [Pure]
        public PlayerTag GetTile(int row, int col) => Board[row][col];

        [Pure]
        public BoardState SetTile(int row, int col, PlayerTag newVal)
        {
            var newBoard = (PlayerTag[][]) Board.Clone();
            newBoard[row][col] = newVal;
            return new BoardState(newBoard);
        }

        #region Generated Equals Implementations

        public bool Equals(BoardState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Board, other.Board);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BoardState) obj);
        }

        public override int GetHashCode()
        {
            return (Board != null ? Board.GetHashCode() : 0);
        }

        #endregion
    }
}