using AReSSO;
using UnityEngine;

namespace TicTacToe.BoardTile
{
    public class TileClickedAction : IAction
    {
        public Vector2Int Location { get; }

        public TileClickedAction(Vector2Int location)
        {
            Location = location;
        }

        public override string ToString() => $"{GetType().Name}: {{ {nameof(Location)}: {Location} }}";
    }
}