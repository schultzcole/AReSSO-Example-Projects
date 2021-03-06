#nullable enable
using System;
using PlayduxExamples.TicTacToe.Scripts.Common;
using PlayduxExamples.TicTacToe.Scripts.State;
using PlayduxExamples.TicTacToe.Scripts.State.Actions;
using PlayduxExamples.TicTacToe.Scripts.State.Selectors;
using TMPro;
using UniRx;
using UnityEngine;

namespace PlayduxExamples.TicTacToe.Scripts.BoardTile
{
    /// A single square on the tic-tac-toe board
    /// This acts as both a producer of TileClickedActions
    /// as well as a consumer of state updates for the specific tile this represents.
    public class BoardTile : MonoBehaviour
    {
        [SerializeField] private GridLocation location;
        [SerializeField] private TicTacToeStore? store;
        [SerializeField] private TextMeshProUGUI? xText;
        [SerializeField] private TextMeshProUGUI? oText;

        /// Subscriptions to the store should be done in Awake.
        private void Awake()
        {
            // the component gets an observable for its corresponding location in the grid, and reacts to any changes
            // to that location's state.
            // ObserveOnMainThread is required to be able to modify Unity GameObjects in the handler.
            store!.ObservableFor(SelectorFor.GridLoc(location)).ObserveOnMainThread().Subscribe(HandleStateChange, Debug.LogError);
        }

        /// Note that we explicitly set this component's relevant state for each case.
        /// This is important because we cannot rely on the existing state of this component being valid in the next
        /// state. It seems a bit verbose, but it is *absolutely* clear what should happen in every possible case.
        private void HandleStateChange(PlayerTag newState)
        {
            switch (newState)
            {
                case PlayerTag.X:
                    xText!.enabled = true;
                    oText!.enabled = false;
                    break;
                case PlayerTag.O:
                    xText!.enabled = false;
                    oText!.enabled = true;
                    break;
                case PlayerTag.None:
                    xText!.enabled = false;
                    oText!.enabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }
        
        /// Called via unity event when the button object in this tile is clicked.
        public void UEventTileClicked()
        {
            if (store!.Select(SelectorFor.GridLoc(location)) == PlayerTag.None)
            {
                store.Dispatch(new TileClickedAction(location));
            }
        }
    }
}