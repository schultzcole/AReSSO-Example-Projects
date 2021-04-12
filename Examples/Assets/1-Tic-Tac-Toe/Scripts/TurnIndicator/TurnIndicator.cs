#nullable enable
using AReSSOExamples.TicTacToe.Scripts.State;
using AReSSOExamples.TicTacToe.Scripts.State.Selectors;
using TMPro;
using UniRx;
using UnityEngine;

namespace AReSSOExamples.TicTacToe.Scripts.TurnIndicator
{
    /// Responsible for updating the UI saying which player's turn it is.
    public class TurnIndicator : MonoBehaviour
    {
        [SerializeField] private TicTacToeStore? store;
        [SerializeField] private TextMeshProUGUI? text;

        /// Subscriptions to the store should be done in Awake.
        private void Awake()
        {
            store!.ObservableFor(Select.CurrentPlayer)
                .Subscribe(currentPlayer => text!.text = $"Turn: {currentPlayer}");
        }
    }
}