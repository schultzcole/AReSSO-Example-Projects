using AReSSOExamples.TicTacToe.Scripts.State;
using AReSSOExamples.TicTacToe.Scripts.State.Actions;
using UnityEngine;

namespace AReSSOExamples.TicTacToe.Scripts.WinModal
{
    public class NewGameButton : MonoBehaviour
    {
        // ReSharper disable RedundantDefaultMemberInitializer
        // initialize to default to remove compiler warning CS0649
        [SerializeField] private TicTacToeStore store = default;
        // ReSharper restore RedundantDefaultMemberInitializer
        
        /// Called via unity event when the new game button is clicked.
        public void UEventNewGameButtonClicked()
        {
            store.Dispatch(new NewGameAction());
        }
    }
}