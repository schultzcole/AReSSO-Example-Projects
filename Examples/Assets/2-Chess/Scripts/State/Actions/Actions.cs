#nullable enable
using Playdux.src.Store;
using PlayduxExamples.Chess.Scripts.Common;

namespace PlayduxExamples.Chess.Scripts.State.Actions
{
    public record MovePieceAction(int Index, ChessLocation NewLocation) : IAction;
}