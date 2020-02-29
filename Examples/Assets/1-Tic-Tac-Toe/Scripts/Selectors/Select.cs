using System;
using TicTacToe.BoardTile;
using TicTacToe.State;

namespace Scripts.Selectors
{
    /// Selectors pull the concern of navigating the state tree out of state consumers and into its own module.
    /// These selectors are quite basic, but in more complicated cases, it can be especially useful to not have to
    /// worry about the exact structure of your state object in your consumers.
    /// Selectors also make it much easier to make alterations to the structure of your state object. When a change
    /// occurs, you only have to change the selector in one place, rather than changing a lambda in every consumer
    /// that is effected by the change.
    public static class Select
    {
        public static PlayerTag CurrentPlayer(TicTacToeState state) => state.CurrentPlayer;
        public static WinState Winner(TicTacToeState state) => state.Winner;
    }
    
    /// These are what I call selector generators. SelectorFor.Tile is not a selector itself, it *returns* a selector.
    public static class SelectorFor
    {
        public static Func<TicTacToeState, PlayerTag> Tile(int row, int col) =>
            state => state.Board.GetTile(row, col);

        public static Func<TicTacToeState, PlayerTag> Tile(GridLocation loc) => Tile(loc.Row, loc.Column);
    }
}