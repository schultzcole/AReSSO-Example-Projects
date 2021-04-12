#nullable enable
using Playdux.src.Store;
using PlayduxExamples.TicTacToe.Scripts.State;
using UnityEngine;

namespace PlayduxExamples.TicTacToe.Scripts.WinModal
{
    public class NewGameButton : MonoBehaviour
    {
        [SerializeField] private TicTacToeStore? store;
        
        /// Called via unity event when the new game button is clicked.
        public void UEventNewGameButtonClicked()
        {
            store!.Dispatch(new InitializeAction<TicTacToeState>(TicTacToeState.InitialState));
        }
    }
}