#nullable enable
using System;
using PlayduxExamples.Chess.Scripts.Common;

namespace PlayduxExamples.Chess.Scripts.State.Selectors
{
    public static class Selector { }

    public static class SelectorGenerator
    {
        public static Func<ChessState, ChessPieceState> ForPiece(int index) =>
            state => state.Pieces[index];
    }
}