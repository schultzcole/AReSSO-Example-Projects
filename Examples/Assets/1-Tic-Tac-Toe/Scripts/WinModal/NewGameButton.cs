#nullable enable
using AReSSOExamples.TicTacToe.Scripts.State;
using AReSSOExamples.TicTacToe.Scripts.State.Actions;
using UnityEngine;

namespace AReSSOExamples.TicTacToe.Scripts.WinModal
{
    public class NewGameButton : MonoBehaviour
    {
        [SerializeField] private TicTacToeStore? store;
        
        /// Called via unity event when the new game button is clicked.
        public void UEventNewGameButtonClicked()
        {
            store!.Dispatch(new NewGameAction());
        }
    }
}