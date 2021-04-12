#nullable enable
using AReSSO;
using AReSSOExamples.TicTacToe.Scripts.Common;

namespace AReSSOExamples.TicTacToe.Scripts.State.Actions
{
    /// Signals that a new game shall begin.
    /// This won't be necessary after AReSSO adds the Initialize action (issue #11 on the AReSSO repo).
    public record NewGameAction : IAction;

    /// An action notifying when a tile is clicked. Action names should follow the convention of ending with "Action"
    public record TileClickedAction(GridLocation Location) : IAction;
}