using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        public void Init()
        {
            _player = GetComponent<Player>();
            _currentPosition = _player.Side == Side.Self ? new Position(1, 1) : new Position(4, 1);
            _player.CurrentTile = MatchManager.Grid.GetTile(_currentPosition, Position.Zero);
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

            if (!targetTile.State.IsSteppable || targetTile.PlayerSide != _player.Side) return false;

            if (_player.Side == Side.Opponent && delta.X != 0) delta.Inverse();
            var moveTime = _player.AnimationHandler.Play(_animationTriggers[delta]);

            Move(targetTile, moveTime);
            return true;
        }

        private async void Move(Tile targetTile, float moveTime)
        {
            IsMoving = true;
            float timer = 0;
            float duration = moveTime == 0 ? _defaultMoveTime : moveTime;
            Vector3 originalPosition = transform.position;
            var targetPosition = targetTile.transform.position;

            var movedToNextTile = false;
            while (timer < duration)
            {
                await Awaitable.NextFrameAsync();
                transform.position = Vector3.Lerp(originalPosition, targetPosition, timer / duration);
                if (timer / duration > 0.6f && !movedToNextTile)
                {
                    _currentPosition = targetTile.Position;
                    movedToNextTile = true;
                    MatchManager.Grid.PlacePlayer(_player, targetTile);
                }

                timer += Time.deltaTime;
            }

            transform.position = targetPosition;
            IsMoving = false;
        }
    }
}