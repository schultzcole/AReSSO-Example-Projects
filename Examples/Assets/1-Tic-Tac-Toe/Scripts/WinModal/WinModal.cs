using System;
using TicTacToe.State;
using TMPro;
using UnityEngine;
using UniRx;

namespace Scripts.WinModal
{
    public class WinModal : MonoBehaviour
    {
        // ReSharper disable RedundantDefaultMemberInitializer
        [SerializeField] private GameObject modalWindow = default;
        [SerializeField] private TicTacToeStore store = default;
        [SerializeField] private TextMeshProUGUI winMessageText = default;
        // ReSharper restore RedundantDefaultMemberInitializer

        private void Awake()
        {
            store.ObservableFor(state => state.Winner)
                .Subscribe(winner =>
                {
                    switch (winner)
                    {
                        case WinState.None:
                            modalWindow.SetActive(false);
                            break;
                        case WinState.X:
                        case WinState.O:
                            modalWindow.SetActive(true);
                            winMessageText.text = $"{winner} won the game!";
                            break;
                        case WinState.Tie:
                            modalWindow.SetActive(true);
                            winMessageText.text = "It's a tie!";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(winner), winner, null);
                    }
                });
        }

        public void OnNewGameButtonClick()
        {
            store.Dispatch(new NewGame());
        }
    }
}