#nullable enable
using System;
using PlayduxExamples.Chess.Scripts.Common;
using PlayduxExamples.Chess.Scripts.State;
using PlayduxExamples.Chess.Scripts.State.Actions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayduxExamples.Chess.Scripts
{
    public class RandomPieceMover : MonoBehaviour
    {
        [SerializeField] private ChessStore? store;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            var pieceIndex = Random.Range(0, store!.State.Pieces.Length - 1);
            var newX = Random.Range(0, Enum.GetValues(typeof(File)).Length - 1);
            var newZ = Random.Range(0, Enum.GetValues(typeof(Rank)).Length - 1);

            store.Dispatch(new MovePieceAction(pieceIndex, (newX, newZ).ChessLocationFromXZ()));
        }
    }
}