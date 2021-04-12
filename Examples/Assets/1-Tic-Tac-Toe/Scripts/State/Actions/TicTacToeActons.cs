#nullable enable
using Playdux.src.Store;
using PlayduxExamples.TicTacToe.Scripts.Common;

namespace PlayduxExamples.TicTacToe.Scripts.State.Actions
{
    /// An action notifying when a tile is clicked. Action names should follow the convention of ending with "Action"
    public record TileClickedAction(GridLocation Location) : IAction;
}