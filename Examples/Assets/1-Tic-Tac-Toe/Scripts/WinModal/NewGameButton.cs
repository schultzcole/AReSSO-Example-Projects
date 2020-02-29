using TicTacToe.State;
using UnityEngine;

namespace Scripts.WinModal
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