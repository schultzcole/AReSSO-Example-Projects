using AReSSO;
using AReSSO.Store;
using TicTacToe.BoardTile;
using UnityEngine;

namespace TicTacToe.State
{
    [DefaultExecutionOrder(-1)]
    public class TicTacToeStore : StoreBehaviour<TicTacToeState>
    {
        protected override Store<TicTacToeState> InitializeStore()
        {
            var initialState = new TicTacToeState(PlayerTag.X);
            return new Store<TicTacToeState>(initialState, RootReducer);
        }

        private static TicTacToeState RootReducer(TicTacToeState state, IAction action)
        {
            Debug.Log($"<b>Action dispatched:</b>\n\t{action}");
            switch (action)
            {
                case NewGame newGame:
                    return new TicTacToeState(PlayerTag.X);
                case TileClickedAction tileClicked:
                    return TileClickedReducer.Reduce(state, tileClicked);
            }

            return state;
        }
    }
}
