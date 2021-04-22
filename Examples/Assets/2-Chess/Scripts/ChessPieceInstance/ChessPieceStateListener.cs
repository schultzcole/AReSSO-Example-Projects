#nullable enable
using System;
using PlayduxExamples.Chess.Scripts.Common;
using UniRx;
using UnityEngine;

namespace PlayduxExamples.Chess.Scripts.ChessPieceInstance
{
    /// Listens to a stream of a chess piece's state and updates the owning game object's position to match.
    public class ChessPieceStateListener : MonoBehaviour
    {
        public void Initialize(IObservable<ChessPieceState> stateStream) =>
            stateStream.Subscribe(
                onNext: state => transform.position = state.Location.ToVector3(),
                Debug.LogException
            );
    }
}
