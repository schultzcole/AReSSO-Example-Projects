#nullable enable
using System;
using PlayduxExamples.Chess.Scripts.Common;
using UniRx;
using UnityEngine;

namespace PlayduxExamples.Chess.Scripts
{
    public class ChessPieceStateListener : MonoBehaviour
    {
        public void Initialize(IObservable<ChessPieceState> stateStream) =>
            stateStream.Subscribe(
                onNext: state => transform.position = state.Location.ToVector3(),
                Debug.LogException
            );
    }
}
