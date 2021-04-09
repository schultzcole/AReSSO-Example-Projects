using AReSSOExamples.TicTacToe.Scripts.Selectors;
using AReSSOExamples.TicTacToe.Scripts.State;
using TMPro;
using UniRx;
using UnityEngine;

namespace AReSSOExamples.TicTacToe.Scripts.TurnIndicator
{
    /// Responsible for updating the UI saying which player's turn it is.
    public class TurnIndicator : MonoBehaviour
    {
        // ReSharper disable RedundantDefaultMemberInitializer
        // initialize to default to remove compiler warning CS0649
        [SerializeField] private TicTacToeStore store = default;
        [SerializeField] private TextMeshProUGUI text = default;
        // ReSharper restore RedundantDefaultMemberInitializer

        /// Subscriptions to the store should be done in Awake.
        private void Awake()
        {
            store.ObservableFor(Select.CurrentPlayer)
                .Subscribe(currentPlayer => text.text = $"Turn: {currentPlayer}");
        }
    }
}