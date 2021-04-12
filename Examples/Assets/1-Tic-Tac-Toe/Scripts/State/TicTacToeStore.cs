#nullable enable
using Playdux.src.Store;
using PlayduxExamples.TicTacToe.Scripts.State.Actions;
using PlayduxExamples.TicTacToe.Scripts.State.Reducers;
using UnityEngine;

namespace PlayduxExamples.TicTacToe.Scripts.State
{
    /// The main store for the TicTacToe game.
    ///
    /// Execution order is important. In future versions of AReSSO this should be set on StoreBehaviour
    [DefaultExecutionOrder(-1)]
    public class TicTacToeStore : StoreBehaviour<TicTacToeState>
    {
        protected override Store<TicTacToeState> InitializeStore()
        {
            return new(TicTacToeState.InitialState, RootReducer);
        }

        /// The root reducer delegates certain action types to different sub-reducers.
        /// If it doesn't have a case for a particular action, it just returns the existing state.
        private static TicTacToeState RootReducer(TicTacToeState state, IAction action)
        {
            Debug.Log($"<b>Action dispatched:</b>\n\t{action}");
            return action switch
            {
                TileClickedAction tileClicked => TileClickedReducer.Reduce(state, tileClicked),
                _ => state
            };
        }
    }
}