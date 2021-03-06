#nullable enable
using PlayduxExamples.TicTacToe.Scripts.State;
using PlayduxExamples.TicTacToe.Scripts.State.Selectors;
using TMPro;
using UniRx;
using UnityEngine;

namespace PlayduxExamples.TicTacToe.Scripts.TurnIndicator
{
    /// Responsible for updating the UI saying which player's turn it is.
    public class TurnIndicator : MonoBehaviour
    {
        [SerializeField] private TicTacToeStore? store;
        [SerializeField] private TextMeshProUGUI? text;

        /// Subscriptions to the store should be done in Awake.
        private void Awake()
        {
            store!.ObservableFor(Select.CurrentPlayer, true)
                .ObserveOnMainThread()
                .Subscribe(currentPlayer => text!.text = $"Turn: {currentPlayer}", Debug.LogError);
        }
    }
}