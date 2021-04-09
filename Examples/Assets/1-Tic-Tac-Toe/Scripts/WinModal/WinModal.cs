using System;
using AReSSOExamples.TicTacToe.Scripts.State;
using AReSSOExamples.TicTacToe.Scripts.State.Selectors;
using TMPro;
using UniRx;
using UnityEngine;

namespace AReSSOExamples.TicTacToe.Scripts.WinModal
{
    /// Controller for the modal window that shows up when the game is over.
    public class WinModal : MonoBehaviour
    {
        // ReSharper disable RedundantDefaultMemberInitializer
        // initialize to default to remove compiler warning CS0649
        [SerializeField] private GameObject modalWindow = default;
        [SerializeField] private TicTacToeStore store = default;
        [SerializeField] private TextMeshProUGUI winMessageText = default;
        // ReSharper restore RedundantDefaultMemberInitializer

        /// Subscriptions to the store should be done in Awake.
        private void Awake()
        {
            store.ObservableFor(Select.Winner)
                .Subscribe(HandleStateChange);
        }

        /// Should be self-explanatory given its simplicity.
        /// Note that the modal window is inactive by default, yet we still explicitly call out that WinState.None
        /// should set the window to inactive.
        /// This is necessary for things to behave well when a new game is created.
        private void HandleStateChange(WinState winner)
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
        }
    }
}