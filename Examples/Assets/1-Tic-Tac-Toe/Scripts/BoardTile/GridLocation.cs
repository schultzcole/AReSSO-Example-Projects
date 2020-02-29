using UnityEngine;

namespace TicTacToe.BoardTile
{
    public class GridLocation : MonoBehaviour
    {
        public int Row;
        public int Column;

        public override string ToString() =>
            $"{{ {nameof(Row)}: {Row} " +
            $"{nameof(Column)}: {Column} }}";
    }
}