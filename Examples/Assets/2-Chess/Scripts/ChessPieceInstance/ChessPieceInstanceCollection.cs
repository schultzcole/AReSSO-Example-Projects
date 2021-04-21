#nullable enable
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Playdux.src.Store;
using PlayduxExamples.Chess.Scripts.Common;
using PlayduxExamples.Chess.Scripts.State;
using PlayduxExamples.Chess.Scripts.State.Selectors;
using UnityEngine;

namespace PlayduxExamples.Chess.Scripts.ChessPieceInstance
{
    public class ChessPieceInstanceCollection : MonoBehaviour, ISideEffector<ChessState>
    {
        [Header("Material Config")]
        [SerializeField] private Material? baseMaterial;
        [SerializeField] private Color whiteColor;
        [SerializeField] private Color blackColor;

        [Header("Piece Prefabs")]
        [SerializeField] private GameObject? pawnPrefab;
        [SerializeField] private GameObject? bishopPrefab;
        [SerializeField] private GameObject? knightPrefab;
        [SerializeField] private GameObject? rookPrefab;
        [SerializeField] private GameObject? queenPrefab;
        [SerializeField] private GameObject? kingPrefab;

        public int Priority => 0;

        private Material? whiteMaterial;
        private Material? blackMaterial;

        private readonly List<GameObject> existingInstanceObjects = new();

        private void Awake()
        {
            whiteMaterial = new Material(baseMaterial!) { color = whiteColor };
            blackMaterial = new Material(baseMaterial!) { color = blackColor };
        }

        public bool PreEffect(DispatchedAction dispatchedAction, IStore<ChessState> store) => true;

        public void PostEffect(DispatchedAction dispatchedAction, IStore<ChessState> store)
        {
            if (dispatchedAction.Action is not InitializeAction<ChessState>) return;

            UniTask.Run(() => SpawnPieces(store));
        }

        private async UniTask SpawnPieces(IStore<ChessState> store)
        {
            // Ensure Awake has been called prior to spawning pieces
            if (whiteMaterial is null || blackMaterial is null) await UniTask.WaitUntil(() => whiteMaterial is not null && blackMaterial is not null);

            ClearExistingObjects();

            var pieces = store.Select(state => state.Pieces);

            for (var i = 0; i < pieces.Length; i++)
            {
                var ((team, piece), loc, _) = pieces[i];

                // Spawn piece instance
                var prefab = PrefabFromPiece(piece);
                var instanceObject = Instantiate(prefab, loc.ToVector3(), Quaternion.identity);
                if (instanceObject is null) throw new InvalidOperationException("Instance of piece not properly created");

                // Set piece orientation (black faces "down", white faces "up")
                if (team is ChessTeam.White) instanceObject.transform.Rotate(Vector3.up, 180f);

                // Apply material
                instanceObject.GetComponentInChildren<MeshRenderer>().material = team switch
                {
                    ChessTeam.Black => blackMaterial!,
                    ChessTeam.White => whiteMaterial!,
                    _ => throw new ArgumentOutOfRangeException(nameof(team), team, null)
                };

                // Initialize instance state listener
                var stateListener = instanceObject.GetComponent<ChessPieceStateListener>();
                if (stateListener is null) throw new InvalidOperationException($"Created instance did not have a component of type {nameof(ChessPieceStateListener)}");

                stateListener.Initialize(store.ObservableFor(SelectorGenerator.ForPiece(i)));
                
                // Initialize instance animator
                var animator = instanceObject.GetComponent<ChessPieceAnimator>();
                if (animator is null) throw new InvalidOperationException($"Created instance did not have a component of type {nameof(ChessPieceAnimator)}");
                
                animator.Initialize(i);

                store.RegisterSideEffector(animator);

                // Store piece instance
                existingInstanceObjects.Add(instanceObject);
            }
        }

        private void ClearExistingObjects()
        {
            foreach (var existing in existingInstanceObjects) { Destroy(existing); }

            existingInstanceObjects.Clear();
        }

        private void OnDestroy()
        {
            Destroy(whiteMaterial);
            Destroy(blackMaterial);
        }

        private GameObject PrefabFromPiece(ChessPiece piece) => piece switch
        {
            ChessPiece.Pawn => pawnPrefab!,
            ChessPiece.Bishop => bishopPrefab!,
            ChessPiece.Knight => knightPrefab!,
            ChessPiece.Rook => rookPrefab!,
            ChessPiece.Queen => queenPrefab!,
            ChessPiece.King => kingPrefab!,
            _ => throw new ArgumentOutOfRangeException(nameof(piece), piece, null)
        };
    }
}