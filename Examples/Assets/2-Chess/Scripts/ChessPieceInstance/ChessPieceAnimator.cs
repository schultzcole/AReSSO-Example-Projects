#nullable enable
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Playdux.src.Store;
using PlayduxExamples.Chess.Scripts.Common;
using PlayduxExamples.Chess.Scripts.State;
using PlayduxExamples.Chess.Scripts.State.Actions;
using UnityEngine;

namespace PlayduxExamples.Chess.Scripts.ChessPieceInstance
{
    public class ChessPieceAnimator : MonoBehaviour, ISideEffector<ChessState>
    {
        [SerializeField] private float AnimationSpeed = 1;
        [SerializeField] private Ease Ease;

        private int? index;

        private Transform? cachedTransform;
        private CancellationToken ct;

        private void Awake()
        {
            cachedTransform = transform;
            ct = this.GetCancellationTokenOnDestroy();
        }

        public void Initialize(int pieceIndex) { index = pieceIndex; }

        // ISideEffector implementation
        public int Priority => 0;

        public bool PreEffect(DispatchedAction dispatchedAction, IStore<ChessState> store)
        {
            if (
                dispatchedAction.IsCanceled
                || dispatchedAction.Action is not MovePieceAction action
                || action.Index != index
                || !action.Animate
            ) { return true; }

            UniTask.Run(() => AnimateTo(action, store), cancellationToken: ct);

            return false;
        }

        private async void AnimateTo(MovePieceAction action, IActionDispatcher<ChessState> dispatcher)
        {
            if (cachedTransform is null) throw new InvalidOperationException($"Unable to animate when {cachedTransform} is null");

            await UniTask.SwitchToMainThread();

            var src = cachedTransform.position;
            var dest = action.NewLocation.ToVector3();
            var deltaX = dest.x - src.x;
            var deltaZ = dest.z - src.z;
            var totalDist = deltaX + deltaZ;

            await cachedTransform.DOPath(new[]
            {
                new Vector3(src.x + deltaX, 0, src.z),
                new Vector3(src.x + deltaX, 0, src.z + deltaZ)
            }, AnimationSpeed).SetEase(Ease);

            dispatcher.Dispatch(action with { Animate = false });
        }

        public void PostEffect(DispatchedAction dispatchedAction, IStore<ChessState> store) { }
    }
}