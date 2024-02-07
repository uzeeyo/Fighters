using System.Collections;
using UnityEngine;

namespace Fighters.Match.Player
{
    public class Movement : MonoBehaviour
    {
        const float TILE_DISTANCE = 1.8f;

        private Vector2 _currentPosition;
        private Player _player;
        private bool _isMoving = false;


        void Start()
        {
            _currentPosition = new Vector2(1, 1);
            _player = GetComponent<Player>();
            StartCoroutine(ListenForMovement());
        }

        private IEnumerator ListenForMovement()
        {
            while (true)
            {
                if (!MatchManager.MatchStarted) yield return null;

                var delta = Vector2.zero;

                if (Input.anyKeyDown)
                {

                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        //delta = new Vector2(-1, 0);
                        GetComponentInChildren<Animator>().SetTrigger("JumpLeft");
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        //delta = new Vector2(1, 0);
                        GetComponentInChildren<Animator>().SetTrigger("JumpRight");
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        //delta = new Vector2(0, 1);
                        GetComponentInChildren<Animator>().SetTrigger("JumpForward");
                        continue;
                    }
                    else if (Input.GetKeyDown(KeyCode.A))
                    {
                        //delta = new Vector2(0, -1);
                        GetComponentInChildren<Animator>().SetTrigger("JumpBackward");
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                    TryMove(delta);
                }
            }
        }

        private IEnumerator Move(Vector3 targetPosition)
        {
            _isMoving = true;
            float duration = 0.8f;
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

        private void TryMove(Vector2 delta)
        {
            if (_isMoving)
            {
                return;
            }

            var targetTile = _player.Grid.GetTile(_currentPosition, delta);

            if (targetTile == null) return;

            if (targetTile.State == Tile.TileState.None)
            {
                _currentPosition += delta;
            }
        }
    }
}

