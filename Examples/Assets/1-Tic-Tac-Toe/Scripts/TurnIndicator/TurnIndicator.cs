using TicTacToe.State;
using TMPro;
using UnityEngine;
using UniRx;

namespace Scripts.TurnIndicator
{
    public class TurnIndicator : MonoBehaviour
    {
        // ReSharper disable RedundantDefaultMemberInitializer
        // initialize to default to remove compiler warning CS0649
        [SerializeField] private TicTacToeStore store = default;
        [SerializeField] private TextMeshProUGUI text = default;
        // ReSharper restore RedundantDefaultMemberInitializer

        private void Awake()
        {
            store.ObservableFor(state => state.CurrentPlayer)
                .Subscribe(currentPlayer => text.text = $"Turn: {currentPlayer}");
        }
    }
}