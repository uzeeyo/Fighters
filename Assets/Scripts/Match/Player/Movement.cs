using Fighters.Match;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private GridPosition _currentPosition;
    private Player _player;
    private bool _isMoving = false;

    [SerializeField] private float _moveSpeed = 20;

    void Start()
    {
        _currentPosition = new GridPosition(1, 1);
        _player = GetComponent<Player>();
        StartCoroutine(ListenForMovement());
    }

    private IEnumerator ListenForMovement()
    {
        while (true)
        {
            if (!MatchManager.MatchStarted) yield return null;

            if (Input.GetKeyDown(KeyCode.W))
            {
                var delta = new GridPosition(-1, 0);
                TryMove(delta);
                yield return null;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                var delta = new GridPosition(1, 0);
                TryMove(delta);
                yield return null;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                var delta = new GridPosition(0, 1);
                TryMove(delta);
                yield return null;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                var delta = new GridPosition(0, -1);
                TryMove(delta);
                yield return null;
            }
        }
    }

    private IEnumerator Move(Vector3 targetPosition)
    {
        _isMoving = true;
        while (Vector3.Distance(transform.position, targetPosition) > 0.1)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        _isMoving = false;
    }

    private void TryMove(GridPosition delta)
    {
        if (_isMoving)
        {
            return;
        }

        var targetTile = _player.TileGrid.GetTile(_currentPosition, delta);

        if (targetTile == null)
        {
            return;
        }
        {

        }
        if (targetTile.State == Tile.TileState.None)
        {
            _currentPosition.X += delta.X;
            _currentPosition.Y += delta.Y;

            StartCoroutine(Move(targetTile.transform.position));
        }
    }
}
