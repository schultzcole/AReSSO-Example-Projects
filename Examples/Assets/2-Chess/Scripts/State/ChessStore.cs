#nullable enable
using Playdux.src.Store;
using UnityEngine;

namespace PlayduxExamples.Chess.Scripts.State
{
    public class ChessStore : StoreBehaviour<ChessState>
    {
        protected override Store<ChessState> InitializeStore()
        {
            return new(ChessState.InitialState, RootReducer);
        }

        private static ChessState RootReducer(ChessState state, IAction action)
        {
            Debug.Log($"<b>Action dispatched:</b>\n\t{action}");
            return action switch
            {
                _ => state
            };
        }
    }
}
