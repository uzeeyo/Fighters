using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fighters.Match.Players
{
    public class MovementController : MonoBehaviour
    {
        private Position _currentPosition;
        private Player _player;
        private bool _isMoving = false;

        private Dictionary<Position, string> _animationTriggers = new()
        {
            { Position.Up, "GMoveLeft" },
            { Position.Down, "GMoveRight" },
            { Position.Left, "GMoveBack" },
            { Position.Right, "GMoveForward" }
        };

        [SerializeField] private float _moveTime = 0.3f;


        void Awake()
        {
            _player = GetComponent<Player>();
            _currentPosition = _player.Side == Side.Self ? new Position(1, 1) : new Position(4, 1);
            _player.CurrentTile = MatchManager.Grid.GetTile(_currentPosition, Position.Zero);
        }

        private void OnMove(InputValue value)
        {
            if (!MatchManager.MatchStarted) return;
            var direction = value.Get<Vector2>();
            TryMove(direction);
        }

        public bool TryMove(Vector2 direction)
        {
            if (_isMoving || !_player.CanInteract || direction == Vector2.zero)
            {
                return false;
            }

            var delta = new Position(direction);
            var targetTile = MatchManager.Grid.GetTile(_currentPosition, delta);

            if (!targetTile) return false;
            
            if (targetTile.State != Tile.TileState.Blocked && targetTile.PlayerSide == _player.Side)
            {
                _currentPosition = targetTile.Location;
                _player.AnimationHandler.Play(_animationTriggers[delta]);
                StartCoroutine(Move(targetTile.transform.position));
            }
            return true;
        }

        private IEnumerator Move(Vector3 targetPosition)
        {
            _isMoving = true;
            float duration = _moveTime;
            float timeElapsed = 0;
            Vector3 currentPosition = transform.position;

            Vector3 path = targetPosition - currentPosition;

            var playerMoved = false;
            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                transform.position = path * (timeElapsed / duration) + currentPosition;
                if (timeElapsed / duration > 0.6f && !playerMoved)
                {
                    playerMoved = true;
                    MatchManager.Grid.PlacePlayer(_player, _currentPosition);
                }

                yield return null;
            }

            transform.position = targetPosition;
            _isMoving = false;
        }
    }
}