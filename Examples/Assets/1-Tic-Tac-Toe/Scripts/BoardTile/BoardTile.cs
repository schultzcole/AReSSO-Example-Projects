using System;
using TicTacToe.State;
using TMPro;
using UnityEngine;
using UniRx;

namespace TicTacToe.BoardTile
{
    public class BoardTile : MonoBehaviour
    {
        // ReSharper disable RedundantDefaultMemberInitializer
        // initialize to default to remove compiler warning CS0649
        [SerializeField] private Vector2Int location = default;
        [SerializeField] private TicTacToeStore store = default;
        [SerializeField] private TextMeshProUGUI xText = default;
        [SerializeField] private TextMeshProUGUI oText = default;
        // ReSharper restore RedundantDefaultMemberInitializer

        private bool filled;
        
        private void Awake()
        {
            store.ObservableFor(state => state.Board.GetTile(location.y, location.x))
                .Subscribe(tileState =>
                {
                    switch (tileState)
                    {
                        case PlayerTag.X:
                            filled = true;
                            xText.enabled = true;
                            oText.enabled = false;
                            break;
                        case PlayerTag.O:
                            filled = true;
                            xText.enabled = false;
                            oText.enabled = true;
                            break;
                        case PlayerTag.None:
                            filled = false;
                            xText.enabled = false;
                            oText.enabled = false;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(tileState), tileState, null);
                    }
                });
        }

        public void OnClick()
        {
            if (!filled)
            {
                store.Dispatch(new TileClickedAction(location));
            }
        }
    }
}