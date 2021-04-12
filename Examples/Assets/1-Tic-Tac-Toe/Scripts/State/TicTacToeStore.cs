#nullable enable
using AReSSO;
using AReSSO.Store;
using AReSSOExamples.TicTacToe.Scripts.State.Actions;
using AReSSOExamples.TicTacToe.Scripts.State.Reducers;
using UnityEngine;

namespace AReSSOExamples.TicTacToe.Scripts.State
{
    /// The main store for the TicTacToe game.
    ///
    /// Execution order is important. In future versions of AReSSO this should be set on StoreBehaviour
    [DefaultExecutionOrder(-1)]
    public class TicTacToeStore : StoreBehaviour<TicTacToeState>
    {
        protected override Store<TicTacToeState> InitializeStore()
        {
            var initialState = new TicTacToeState(PlayerTag.X);
            return new Store<TicTacToeState>(initialState, RootReducer);
        }

        /// The root reducer delegates certain action types to different sub-reducers.
        /// If it doesn't have a case for a particular action, it just returns the existing state.
        private static TicTacToeState RootReducer(TicTacToeState state, IAction action)
        {
            Debug.Log($"<b>Action dispatched:</b>\n\t{action}");
            switch (action)
            {
                case NewGameAction _:
                    return new TicTacToeState(PlayerTag.X);
                case TileClickedAction tileClicked:
                    return TileClickedReducer.Reduce(state, tileClicked);
            }

            return state;
        }
    }
}