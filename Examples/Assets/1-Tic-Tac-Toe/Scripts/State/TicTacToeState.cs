using System;
using AReSSO.CopyUtils;

namespace TicTacToe.State
{
    /// Root state object for the tic tac toe game.
    /// Note that it implements IEquatable. This is required so that the Store will know when any property of the
    /// state changes.
    public class TicTacToeState : IEquatable<TicTacToeState>
    {
        public BoardState Board { get; }
        public PlayerTag CurrentPlayer { get; }
        public WinState Winner { get; }

        /// Public constructor just needs to know which player is starting. Other properties have known defaults.
        public TicTacToeState(PlayerTag startingPlayer)
        {
            Board = new BoardState();
            CurrentPlayer = startingPlayer;
            Winner = WinState.None;
        }

        /// Private constructor for copying.
        private TicTacToeState(BoardState board, PlayerTag currentPlayer, WinState winner)
        {
            Board = board;
            CurrentPlayer = currentPlayer;
            Winner = winner;
        }

        /// Produces a copy of the TicTacToeState.
        /// PropertyChange is a utility struct provided in the AReSSO namespace that makes it simple to write Copy
        /// methods.
        public TicTacToeState Copy(
            PropertyChange<BoardState> board = default,
            PropertyChange<PlayerTag> currentPlayer = default,
            PropertyChange<WinState> winner = default)
        {
            return new TicTacToeState(board.Else(Board), currentPlayer.Else(CurrentPlayer), winner.Else(Winner));
        }

        #region Generated Equals Implementations

        public bool Equals(TicTacToeState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Board, other.Board) && CurrentPlayer == other.CurrentPlayer && Winner == other.Winner;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TicTacToeState) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Board != null ? Board.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) CurrentPlayer;
                hashCode = (hashCode * 397) ^ Winner.GetHashCode();
                return hashCode;
            }
        }

        #endregion

    }
    
    /// There appears to be significant overlap between these enums, but they are semantically different.
    /// Combining them would be an error and would lead to impossible state being possible.
    public enum PlayerTag { None, X, O }
    public enum WinState { None, X, O, Tie }

    public static class PlayerTagExtensions
    {
        /// Easy converter to convert between player tag and win state.
        /// Note that PlayerTag.None maps to different output values based on the allFilled parameter.
        public static WinState ToWinState(this PlayerTag player, bool allFilled)
        {
            switch (player)
            {
                case PlayerTag.None:
                    return allFilled ? WinState.Tie : WinState.None;
                case PlayerTag.X:
                    return WinState.X;
                case PlayerTag.O:
                    return WinState.X;
                default:
                    throw new ArgumentOutOfRangeException(nameof(player), player, null);
            }
        }
    }
}