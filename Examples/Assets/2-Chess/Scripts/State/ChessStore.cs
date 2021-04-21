#nullable enable
using Playdux.src.Store;
using UnityEngine;

namespace PlayduxExamples.Chess.Scripts.State
{
    public class ChessStore : StoreBehaviour<ChessState>
    {
        [SerializeField] private ChessPieceSpawner? spawner;
        
        protected override Store<ChessState> InitializeStore() => new(ChessState.InitialState, RootReducer, new[] { spawner! });

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
