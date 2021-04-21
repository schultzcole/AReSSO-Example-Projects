#nullable enable
using PlayduxExamples.Chess.Scripts.Common;
using static PlayduxExamples.Chess.Scripts.Common.ChessPiece;
using static PlayduxExamples.Chess.Scripts.Common.ChessTeam;

namespace PlayduxExamples.Chess.Scripts.State
{
    public record ChessState(ChessTeam CurrentTeam, ChessPieceState[] Pieces)
    {
        public static ChessState InitialState => new(White, new ChessPieceState[]
        {
            // White Pieces
            new((White, Pawn), (Rank.Two, File.A)),
            new((White, Pawn), (Rank.Two, File.B)),
            new((White, Pawn), (Rank.Two, File.C)),
            new((White, Pawn), (Rank.Two, File.D)),
            new((White, Pawn), (Rank.Two, File.E)),
            new((White, Pawn), (Rank.Two, File.F)),
            new((White, Pawn), (Rank.Two, File.G)),
            new((White, Pawn), (Rank.Two, File.H)),
            new((White, Rook), (Rank.One, File.A)),
            new((White, Knight), (Rank.One, File.B)),
            new((White, Bishop), (Rank.One, File.C)),
            new((White, Queen), (Rank.One, File.D)),
            new((White, King), (Rank.One, File.E)),
            new((White, Bishop), (Rank.One, File.F)),
            new((White, Knight), (Rank.One, File.G)),
            new((White, Rook), (Rank.One, File.H)),
            // Black Pieces
            new((Black, Pawn), (Rank.Seven, File.A)),
            new((Black, Pawn), (Rank.Seven, File.B)),
            new((Black, Pawn), (Rank.Seven, File.C)),
            new((Black, Pawn), (Rank.Seven, File.D)),
            new((Black, Pawn), (Rank.Seven, File.E)),
            new((Black, Pawn), (Rank.Seven, File.F)),
            new((Black, Pawn), (Rank.Seven, File.G)),
            new((Black, Pawn), (Rank.Seven, File.H)),
            new((Black, Rook), (Rank.Eight, File.A)),
            new((Black, Knight), (Rank.Eight, File.B)),
            new((Black, Bishop), (Rank.Eight, File.C)),
            new((Black, Queen), (Rank.Eight, File.D)),
            new((Black, King), (Rank.Eight, File.E)),
            new((Black, Bishop), (Rank.Eight, File.F)),
            new((Black, Knight), (Rank.Eight, File.G)),
            new((Black, Rook), (Rank.Eight, File.H)),
        });
    }
}