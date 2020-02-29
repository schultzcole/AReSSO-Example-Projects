using Scripts.Selectors;
using TicTacToe.State;
using UnityEngine;

namespace TicTacToe.BoardTile
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