#nullable enable
using System;
using System.Linq;
using AReSSOExamples.TicTacToe.Scripts.Common;
using AReSSOExamples.TicTacToe.Scripts.State.Actions;

namespace AReSSOExamples.TicTacToe.Scripts.State.Reducers
{
    /// <summary>
    /// A set of reducers that react to the TileClicked Action.
    /// </summary>
    /// <remarks>
    /// There is no prescription for organization of reducers. They can be organized however makes sense to you.
    /// In this case, I have organized all of the reducers that react to the TileClicked action into a single static
    /// class.
    ///
    /// It is good to maintain separation of concerns for reducers. Notice how the "sub-reducers" here
    /// all react to the same action, yet perform different, discrete tasks.
    /// </remarks>
    public static class TileClickedReducer
    {
        /// This reducer aggregates the sub-reducers in this class.
        /// We use an aggregate reducer like this one to retain control of the order in which the sub-reducers are
        /// called.
        public static TicTacToeState Reduce(TicTacToeState state, TileClickedAction action)
        {
            var newBoard = NextBoard(state.Board, state.CurrentPlayer, action.Location);
            var winner = DetermineWinner(newBoard);
            var gameOver = winner != WinState.None;
            var nextPlayer = NextPlayer(state.CurrentPlayer, gameOver);
            return state with { Board = newBoard, CurrentPlayer = nextPlayer, Winner = winner };
        }

        /// This reducer updates the board to the next state based on the current player and the location
        /// that was clicked.
        /// 
        /// Notice that this only returns the new board, it does not return the entire state. This is to give us
        /// some reassurance that it is only modifying the state that it is responsible for.
        private static BoardState NextBoard(BoardState board, PlayerTag currentPlayer, GridLocation location) =>
            board.SetTile(location.Row, location.Column, currentPlayer);

        /// This reducer takes a board state and determines if there is a winner.
        private static WinState DetermineWinner(BoardState board)
        {
            var rows = Enumerable.Range(0, BoardState.HEIGHT).Select(board.GetRow);
            var cols = Enumerable.Range(0, BoardState.HEIGHT).Select(board.GetCol);
            var diags = new[]
            {
                new[] { board.GetGridLoc(0, 0), board.GetGridLoc(1, 1), board.GetGridLoc(2, 2) },
                new[] { board.GetGridLoc(0, 2), board.GetGridLoc(1, 1), board.GetGridLoc(2, 0) }
            };

            var winner = new[] { rows, cols, diags }
                .SelectMany(lanes => lanes)
                .Select(lane =>
                    lane.Aggregate((PlayerTag?) null, (acc, next) =>
                        {
                            if (acc == null || acc == next) return next;
                            return PlayerTag.None;
                        }
                    ) ?? PlayerTag.None)
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
        private static PlayerTag NextPlayer(PlayerTag currentPlayer, bool gameOver) => (gameOver, currentPlayer) switch
        {
            (true, _) => PlayerTag.None,
            (_, PlayerTag.X) => PlayerTag.O,
            (_, PlayerTag.O) => PlayerTag.X,
            (_, PlayerTag.None) => PlayerTag.X,
            _ => throw new ArgumentOutOfRangeException(nameof(currentPlayer), currentPlayer, null)
        };
    }
}