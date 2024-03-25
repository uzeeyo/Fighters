using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fighters.Match.Players
{
    public class Movement : MonoBehaviour
    {
        const float TILE_DISTANCE = 1.8f;

        private Vector2 _currentPosition;
        private Player _player;
        private bool _isMoving = false;


        void Awake()
        {
            _currentPosition = new Vector2(1, 1);
            _player = GetComponent<Player>();
            _player.CurrentTile = _player.Grid.GetTile(_currentPosition, Vector2.zero);
        }

        private void OnMove(InputValue value)
        {
            //if (!MatchManager.MatchStarted) return;
            var direction = value.Get<Vector2>();


            if (_isMoving)
            {
                return;
            }

            var targetTile = _player.Grid.GetTile(_currentPosition, direction);

            if (targetTile == null) return;

            if (targetTile.State == Tile.TileState.None)
            {
                _currentPosition = targetTile.Location;
                _player.CurrentTile = targetTile;
                StartCoroutine(Move(targetTile.transform.position));
            }
        }

        private IEnumerator Move(Vector3 targetPosition)
        {
            _isMoving = true;
            float duration = 0.6f;
            float timeElapsed = 0;
            Vector3 currentPosition = transform.position;

            while (Vector3.Distance(currentPosition, targetPosition) > 0.1f)
            {
                timeElapsed += Time.deltaTime;
                currentPosition = Vector3.Lerp(currentPosition, targetPosition, timeElapsed / duration);
                transform.position = currentPosition;
                yield return null;
            }
            transform.position = targetPosition;
            _isMoving = false;
        }
    }
}

