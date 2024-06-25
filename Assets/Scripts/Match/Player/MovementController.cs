using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fighters.Match.Players
{
    public class MovementController : MonoBehaviour
    {
        private Animator _animator;
        private Vector2 _currentPosition;
        private Player _player;
        private bool _isMoving = false;
        private Dictionary<Vector2, string> _animationTriggers = new()
        {
            { Vector2.up, "MoveLeft" },
            { Vector2.down, "MoveRight" },
            { Vector2.left, "MoveBack" },
            { Vector2.right, "MoveForward" }
        };

        [SerializeField] private float _moveTime = 0.3f;


        void Awake()
        {
            _currentPosition = new Vector2(1, 1);
            _player = GetComponent<Player>();
            _player.CurrentTile = _player.Grid.GetTile(_currentPosition, Vector2.zero);
            _animator = GetComponentInChildren<Animator>();
        }

        private void OnMove(InputValue value)
        {
            //if (!MatchManager.MatchStarted) return;
            var direction = value.Get<Vector2>();
            TryMove(direction);
        }

        public bool TryMove(Vector2 direction)
        {
            if (_isMoving || !_player.CanInteract || direction == Vector2.zero)
            {
                return false;
            }

            var targetTile = _player.Grid.GetTile(_currentPosition, direction);

            if (targetTile == null) return false;

            if (targetTile.State == Tile.TileState.None && targetTile.Grid.Owner == _player.Side)
            {
                _currentPosition = targetTile.Location;
                _player.CurrentTile = targetTile;
                //_animator.SetTrigger(_animationTriggers[direction]);
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

            while (timeElapsed < duration)
            {
                timeElapsed += Time.deltaTime;
                transform.position = path * (timeElapsed / duration) + currentPosition;
                yield return null;
            }
            transform.position = targetPosition;
            _isMoving = false;
        }
    }
}

