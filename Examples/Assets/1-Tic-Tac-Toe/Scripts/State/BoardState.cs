#nullable enable
using System.Diagnostics.Contracts;
using System.Linq;
using AReSSOExamples.TicTacToe.Scripts.Common;

namespace AReSSOExamples.TicTacToe.Scripts.State
{
    /// Holds on to the board state and exposes it in a friendly interface.
    public record BoardState
    {
        public const int WIDTH = 3;
        public const int HEIGHT = 3;

        private PlayerTag[][] board;

        /// The publicly exposed board is always a copy of the true board state. This way, no external code can
        /// accidentally modify the real board state and screw things up.
        /// Be careful when copying nested lists.
        public PlayerTag[][] Board => board.Select(row => (PlayerTag[]) row.Clone()).ToArray();

        /// The public constructor is used to create a new blank board. Code that creates a new board doesn't need
        /// to specify any initial state, that can be created from scratch.
        public BoardState()
        {
            board = new PlayerTag[HEIGHT][];
            for (int row = 0; row < HEIGHT; row++)
            {
                board[row] = new PlayerTag[WIDTH];
            }
        }

        /// An easy accessor for a particular row of the board.
        /// The use of the Pure attribute makes it clear to external code that this won't modify state and is therefore
        /// safe to use in reducers.
        /// The attribute doesn't actually enforce anything, it is solely informational.
        [Pure]
        public PlayerTag[] GetRow(int row) => Board[row];

        /// An easy accessor for a particular column of the board.
        [Pure]
        public PlayerTag[] GetCol(int col) => Enumerable.Range(0, HEIGHT).Select(row => Board[row][col]).ToArray();

        /// An easy accessor for a particular location in the grid.
        [Pure]
        public PlayerTag GetGridLoc(int row, int col) => Board[row][col];

        [Pure]
        public PlayerTag GetGridLoc(GridLocation location) =>
            GetGridLoc(location.Row, location.Column);

        /// A "setter". This doesn't actually modify the current state, it returns a new state with the modification.
        [Pure]
        public BoardState SetTile(int row, int col, PlayerTag newVal)
        {
            var newBoard = (PlayerTag[][]) Board.Clone();
            newBoard[row][col] = newVal;
            return this with { board = newBoard };
        }
    }
}