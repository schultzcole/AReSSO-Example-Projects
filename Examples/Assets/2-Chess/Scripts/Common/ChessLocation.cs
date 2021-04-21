#nullable enable
using System;
using UnityEngine;

namespace PlayduxExamples.Chess.Scripts.Common
{
    public record ChessLocation(Rank Rank, File File)
    {
        public static implicit operator ChessLocation(ValueTuple<Rank, File> tuple) => new(tuple.Item1, tuple.Item2);
    }

    public enum Rank { One, Two, Three, Four, Five, Six, Seven, Eight }

    public enum File { A, B, C, D, E, F, G, H }
    
    public static class RankAndFileExtensions
    {
        public static int ToXPos(this File file) => (int)file;
        public static int ToZPos(this Rank rank) => (int)rank;

        public static Vector3 ToVector3(this ChessLocation loc) => new(loc.File.ToXPos(), 0, loc.Rank.ToZPos());
    }
}