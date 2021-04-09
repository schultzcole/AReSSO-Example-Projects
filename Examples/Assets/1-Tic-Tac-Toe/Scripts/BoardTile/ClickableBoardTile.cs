using AReSSOExamples.TicTacToe.Scripts.State;
using AReSSOExamples.TicTacToe.Scripts.State.Actions;
using AReSSOExamples.TicTacToe.Scripts.State.Selectors;
using UnityEngine;

namespace AReSSOExamples.TicTacToe.Scripts.BoardTile
{
    public class ClickableBoardTile : MonoBehaviour
    {
        // ReSharper disable RedundantDefaultMemberInitializer
        // initialize to default to remove compiler warning CS0649
        [SerializeField] private GridLocation location = default;
        [SerializeField] private TicTacToeStore store = default;
        // ReSharper restore RedundantDefaultMemberInitializer

        /// Called via unity event when the button object in this tile is clicked.
        public void UEventTileClicked()
        {
            if (store.Select(SelectorFor.Tile(location)) == PlayerTag.None)
            {
                store.Dispatch(new TileClickedAction(location));
            }
        }
    }
}