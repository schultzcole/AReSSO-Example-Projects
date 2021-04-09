using System;
using AReSSOExamples.TicTacToe.Scripts.Common;
using AReSSOExamples.TicTacToe.Scripts.State;
using AReSSOExamples.TicTacToe.Scripts.State.Actions;
using AReSSOExamples.TicTacToe.Scripts.State.Selectors;
using TMPro;
using UniRx;
using UnityEngine;

namespace AReSSOExamples.TicTacToe.Scripts.BoardTile
{
    /// A single square on the tic-tac-toe board
    /// This acts as both a producer of TileClickedActions
    /// as well as a consumer of state updates for the specific tile this represents.
    public class BoardTile : MonoBehaviour
    {
        // ReSharper disable RedundantDefaultMemberInitializer
        // initialize to default to remove compiler warning CS0649
        [SerializeField] private GridLocation location = default;
        [SerializeField] private TicTacToeStore store = default;
        [SerializeField] private TextMeshProUGUI xText = default;
        [SerializeField] private TextMeshProUGUI oText = default;
        // ReSharper restore RedundantDefaultMemberInitializer

        /// Subscriptions to the store should be done in Awake.
        private void Awake()
        {
            // the component gets an observable for its corresponding location in the grid, and reacts to any changes
            // to that location's state.
            store.ObservableFor(SelectorFor.GridLoc(location)).Subscribe(HandleStateChange);
        }

        /// Note that we explicitly set this component's relevant state for each case.
        /// This is important because we cannot rely on the existing state of this component being valid in the next
        /// state. It seems a bit verbose, but it is *absolutely* clear what should happen in every possible case.
        private void HandleStateChange(PlayerTag newState)
        {
            switch (newState)
            {
                case PlayerTag.X:
                    xText.enabled = true;
                    oText.enabled = false;
                    break;
                case PlayerTag.O:
                    xText.enabled = false;
                    oText.enabled = true;
                    break;
                case PlayerTag.None:
                    xText.enabled = false;
                    oText.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }
        
        /// Called via unity event when the button object in this tile is clicked.
        public void UEventTileClicked()
        {
            if (store.Select(SelectorFor.GridLoc(location)) == PlayerTag.None)
            {
                store.Dispatch(new TileClickedAction(location));
            }
        }
    }
}