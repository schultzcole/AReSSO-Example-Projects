#nullable enable
using System;

namespace PlayduxExamples.TicTacToe.Scripts.State
{
    /// Root state object for the tic tac toe game.
    /// Note that it implements IEquatable. This is required so that the Store will know when any property of the
    /// state changes.
    public record TicTacToeState(BoardState Board, PlayerTag CurrentPlayer, WinState Winner)
    {
        /// Public constructor just needs to know which player is starting. Other properties have known defaults.
        public TicTacToeState(PlayerTag startingPlayer) : this(new BoardState(), startingPlayer, WinState.None) { }

        public static TicTacToeState InitialState => new(PlayerTag.X);
    }

    /// There appears to be significant overlap between these enums, but they are semantically different.
    /// Combining them would be an error and would lead to impossible state being possible.
    public enum PlayerTag { None, X, O }

    public enum WinState { None, X, O, Tie }

    public static class PlayerTagExtensions
    {
        /// Easy converter to convert between player tag and win state.
        /// Note that PlayerTag.None maps to different output values based on the allFilled parameter.
        public static WinState ToWinState(this PlayerTag player, bool allFilled) => player switch
        {
            PlayerTag.None => allFilled ? WinState.Tie : WinState.None,
            PlayerTag.X => WinState.X,
            PlayerTag.O => WinState.O,
            _ => throw new ArgumentOutOfRangeException(nameof(player), player, null)
        };
    }
}