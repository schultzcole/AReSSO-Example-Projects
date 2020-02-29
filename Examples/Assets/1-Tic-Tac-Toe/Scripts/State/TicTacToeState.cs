using System;
using AReSSO.CopyUtils;

namespace TicTacToe.State
{
    public class TicTacToeState : IEquatable<TicTacToeState>
    {
        public BoardState Board { get; }
        public PlayerTag CurrentPlayer { get; }
        public WinState Winner { get; }

        public TicTacToeState(PlayerTag startingPlayer)
        {
            Board = new BoardState();
            CurrentPlayer = startingPlayer;
            Winner = WinState.None;
        }

        private TicTacToeState(BoardState board, PlayerTag currentPlayer, WinState winner)
        {
            Board = board;
            CurrentPlayer = currentPlayer;
            Winner = winner;
        }

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
    
    public enum PlayerTag { None, X, O }
    public enum WinState { None, X, O, Tie }

    public static class PlayerTagExtensions
    {
        public static WinState ToWinState(this PlayerTag player, bool allFilled)
        {
            switch (player)
            {
                case PlayerTag.None:
                    return allFilled ? WinState.Tie: WinState.None;
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