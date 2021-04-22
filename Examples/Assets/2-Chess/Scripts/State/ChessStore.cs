#nullable enable
using Playdux.src.Store;
using PlayduxExamples.Chess.Scripts.ChessPieceInstance;
using PlayduxExamples.Chess.Scripts.State.Actions;
using PlayduxExamples.Chess.Scripts.State.Reducers;
using UnityEngine;

namespace PlayduxExamples.Chess.Scripts.State
{
    public class ChessStore : StoreBehaviour<ChessState>
    {
        [SerializeField] private ChessPieceInstanceCollection? spawner;
        
        protected override Store<ChessState> InitializeStore() => new(ChessState.InitialState, RootReducer, new[] { spawner! });

        private static ChessState RootReducer(ChessState state, IAction action)
        {
            Debug.Log($"<b>Action dispatched:</b>\n\t{action}");
            return action switch
            {
                MovePieceAction a => state with { Pieces = MovePieceReducer.Reduce(state.Pieces, a) },
                _ => state
            };
        }
    }
}
