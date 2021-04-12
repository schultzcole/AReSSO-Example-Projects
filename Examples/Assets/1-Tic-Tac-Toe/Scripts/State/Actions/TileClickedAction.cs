#nullable enable
using AReSSO;
using AReSSOExamples.TicTacToe.Scripts.Common;

namespace AReSSOExamples.TicTacToe.Scripts.State.Actions
{
    /// An action notifying when a tile is clicked. Action names should follow the convention of ending with "Action"
    public class TileClickedAction : IAction
    {
        public GridLocation Location { get; }

        public TileClickedAction(GridLocation location)
        {
            Location = location;
        }

        /// This to string implementation will eventually be replaced with a generic version on the Action base class
        /// Ideally, once the devtools are ready, printing out actions to the logs will not be necessary.
        public override string ToString() => $"{GetType().Name}: {{ {nameof(Location)}: {Location} }}";
    }
}