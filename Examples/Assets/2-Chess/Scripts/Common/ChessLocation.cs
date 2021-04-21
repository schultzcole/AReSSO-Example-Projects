#nullable enable
using System;

namespace PlayduxExamples.Chess.Scripts.Common
{
    public record ChessLocation(Rank Rank, File File)
    {
        public static implicit operator ChessLocation(ValueTuple<Rank, File> tuple) => new(tuple.Item1, tuple.Item2);
    }

    public enum Rank { One, Two, Three, Four, Five, Six, Seven, Eight }

    public enum File { A, B, C, D, E, F, G, H }
}