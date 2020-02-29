using System;
using System.Linq;
using TicTacToe.State;

namespace TicTacToe.BoardTile
{
    /// <summary>
    /// A set of reducers that react to the TileClicked Action.
    /// </summary>
    /// <remarks>
    /// There is no prescription for organization of reducers. They can be organized however makes sense to you.
    /// In this case, I have organized all of the reducers that react to the TileClicked action into a single static
    /// class.
    ///
    /// It is good to maintain separation of concerns for reducers (for all things, really). Notice how these reducers
    /// all react to the same action, yet perform different tasks.
    /// </remarks>
    
    public static class TileClickedReducer
    {
        /// This reducer aggregates the sub-reducers in this class.
        /// We use an aggregate reducer like this one to retain control of the order in which the sub-reducers are
        /// called.
        public static TicTacToeState Reduce(TicTacToeState state, TileClickedAction action)
        {
            var newBoard = NextBoard(state.Board, state.CurrentPlayer, action);
            var winner = DetermineWinner(newBoard);
            var gameOver = winner != WinState.None;
            var nextPlayer = NextPlayer(state.CurrentPlayer, gameOver);
            return state.Copy(newBoard, nextPlayer, winner);
        }
        
        /// This reducer updates the board to the next state based on the current player and the location
        /// that was clicked.
        /// 
        /// Notice that this only returns the new board, it does not return the entire state. This is to give us
        /// some reassurance that it is only modifying the state that it is responsible for.
        private static BoardState NextBoard(BoardState board, PlayerTag currentPlayer, TileClickedAction action)
        {
            var (col, row) = (action.Location.x, action.Location.y);
            return board.SetTile(row, col, currentPlayer);
        }

        private static WinState DetermineWinner(BoardState board)
        {
            var rows = Enumerable.Range(0, BoardState.HEIGHT).Select(board.GetRow);
            var cols = Enumerable.Range(0, BoardState.HEIGHT).Select(board.GetCol);
            var diags = new[]
            {
                new[] { board.GetTile(0, 0), board.GetTile(1, 1), board.GetTile(2, 2) },
                new[] { board.GetTile(0, 2), board.GetTile(1, 1), board.GetTile(2, 0) }
            };

            var winner = rows.Concat(cols).Concat(diags)
                .Select(lane => lane.Aggregate((PlayerTag?) null, (acc, next) =>
                {
                    if (acc == null || acc == next) return next;
                    return PlayerTag.None;
                }) ?? PlayerTag.None)
                .FirstOrDefault(laneSame => laneSame != PlayerTag.None);
            var allFilled = rows.SelectMany(row => row).All(tile => tile != PlayerTag.None);

            return winner.ToWinState(allFilled);
        }

        /// This reducer switches to the next player when a tile is clicked.
        /// 
        /// Notice that it doesn't do fancy looping to get the next PlayerTag. It just does the dumbest, most basic
        /// thing that will accomplish its goal. In a more complicated game with more players, or a variable number of
        /// players, more advanced logic would be necessary, but that is not necessary here and would just
        /// make it more difficult to understand.
        private static PlayerTag NextPlayer(PlayerTag currentPlayer, bool gameOver)
        {
            if (gameOver)
            {
                return PlayerTag.None;
            }
            switch (currentPlayer)
            {
                case PlayerTag.X:
                    return PlayerTag.O;
                case PlayerTag.O:
                    return PlayerTag.X;
                case PlayerTag.None:
                    return PlayerTag.X;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentPlayer), currentPlayer, null);
            }
        }
    }
}