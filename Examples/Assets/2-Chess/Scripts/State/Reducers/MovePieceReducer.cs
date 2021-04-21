using PlayduxExamples.Chess.Scripts.Common;
using PlayduxExamples.Chess.Scripts.State.Actions;

namespace PlayduxExamples.Chess.Scripts.State.Reducers
{
    public static class MovePieceReducer
    {
        public static ChessPieceState[] Reduce(ChessPieceState[] pieces, MovePieceAction action) =>
            PiecesReducerUtils.SetChessPieceState(pieces, action.Index, pieces[action.Index] with { Location = action.NewLocation });
    }
}