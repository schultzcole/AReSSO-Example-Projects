using System;
using PlayduxExamples.Chess.Scripts.Common;

namespace PlayduxExamples.Chess.Scripts.State.Reducers
{
    public static class PiecesReducerUtils
    {
        public static ChessPieceState[] SetChessPieceState(ChessPieceState[] pieces, int index, ChessPieceState newState)
        {
            var newPieces = new ChessPieceState[pieces.Length];

            Array.Copy(pieces, 0, newPieces, 0, index);
            newPieces[index] = newState;
            Array.Copy(pieces, index + 1, newPieces, index + 1, pieces.Length - (index + 1));

            return newPieces;
        }
    }
}