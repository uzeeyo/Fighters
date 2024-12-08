using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Fighters.Match.Players
{
    public class MovementController : MonoBehaviour
    {
        private Position _currentPosition;
        private Player _player;
        private Dictionary<Position, string> _animationTriggers = new()
        {
            { Position.Up, "MoveLeft" },
            { Position.Down, "MoveRight" },
            { Position.Left, "MoveBack" },
            { Position.Right, "MoveForward" }
        };
        
        [SerializeField] private float _defaultMoveTime = 0.3f;

        public bool IsMoving { get; private set; }

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
            if (!_player.CanMove || direction == Vector2.zero)
            {
                return false;
            }

            var delta = new Position(direction);
            var targetTile = MatchManager.Grid.GetTile(_currentPosition, delta);

            if (!targetTile) return false;
            
            if (targetTile.State != Tile.TileState.Blocked && targetTile.PlayerSide == _player.Side)
            {
                _currentPosition = targetTile.Location;
                var moveTime = _player.AnimationHandler.Play(_animationTriggers[delta]);
                Move(targetTile.transform.position, moveTime);
            }
            return true;
        }

        private async void Move(Vector3 targetPosition, float moveTime)
        {
            IsMoving = true;
            float duration = moveTime == 0 ? _defaultMoveTime : moveTime;
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

                await Awaitable.NextFrameAsync();
            }

            transform.position = targetPosition;
            IsMoving = false;
        }
    }
}