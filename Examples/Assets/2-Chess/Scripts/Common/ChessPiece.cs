#nullable enable
using System;

namespace PlayduxExamples.Chess.Scripts.Common
{
    public record ChessPieceState(ChessPieceIdentity Identity, ChessLocation Location, bool Alive = true);

    public record ChessPieceIdentity(ChessTeam Team, ChessPiece Piece)
    {
        public static implicit operator ChessPieceIdentity(ValueTuple<ChessTeam, ChessPiece> tuple) => new(tuple.Item1, tuple.Item2);
    }

    public enum ChessTeam { Black, White }

    public enum ChessPiece { Pawn, Bishop, Knight, Rook, Queen, King }

    static class ChessPieceExtensions
    {
        public static int Value(this ChessPiece piece) => piece switch
        {
            ChessPiece.Pawn => 1,
            ChessPiece.Bishop => 3,
            ChessPiece.Knight => 3,
            ChessPiece.Rook => 5,
            ChessPiece.Queen => 9,
            ChessPiece.King => int.MaxValue,
            _ => throw new ArgumentOutOfRangeException(nameof(piece), piece, $"Unrecognized ChessPiece {piece}")
        };
    }
}