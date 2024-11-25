using System.Collections.Generic;
using System.Linq;
using Fighters.Match.Players;
using UnityEngine;

namespace Fighters.Match
{
    public class TileGrid : MonoBehaviour
    {
        const int GRID_X_SIZE = 6;
        const int GRID_Y_SIZE = 3;

        [SerializeField] private GameObject _tileParentObject;

        private readonly Tile[,] _tilesTwoD = new Tile[GRID_X_SIZE, GRID_Y_SIZE];

        private void Awake()
        {
            var tileObjects = new GameObject[GRID_X_SIZE * GRID_Y_SIZE];
            for (int i = 0; i < GRID_X_SIZE * GRID_Y_SIZE; i++)
            {
                tileObjects[i] = _tileParentObject.transform.GetChild(i).gameObject;
            }

            int index = 0;
            for (int y = GRID_Y_SIZE - 1; y >= 0; y--)
            {
                for (int x = 0; x < GRID_X_SIZE; x++)
                {
                    var tileObj = new GameObject("Tile", typeof(Tile));
                    tileObj.transform.SetParent(transform);
                    var tile = tileObj.GetComponent<Tile>();
                    tile.transform.position = tileObjects[index].transform.position;
                    tile.TileObject = tileObjects[index];
                    tile.Init(new Position(x, y));
                    tile.PlayerSide = x > 2 ? Side.Opponent : Side.Self;
                    _tilesTwoD[x, y] = tile;
                    index++;
                }
            }
        }

        public bool IsValidPosition(Position position)
        {
            int x = position.X;
            int y = position.Y;
            return x >= 0 && x < GRID_X_SIZE && y >= 0 && y < GRID_Y_SIZE;
        }

        public Tile GetTile(Position start, Position delta)
        {
            int x = start.X + delta.X;
            int y = start.Y + delta.Y;

            if (!IsValidPosition(new Position(x, y))) return null;

            return _tilesTwoD[x, y];
        }

        public Tile GetRandomTile(Side side)
        {
            return _tilesTwoD.Cast<Tile>()
                .Where(tile => side == Side.Both || tile.PlayerSide == side)
                .OrderBy(x => UnityEngine.Random.value)
                .FirstOrDefault();
        }

        public List<Tile> GetTilesInDirection(Position origin, Position direction, int range)
        {
            List<Tile> tiles = new List<Tile>();
            var currentTile = GetTile(origin, direction);
            int tileIndex = 1;

            while (currentTile != null && tileIndex < range)
            {
                tiles.Add(currentTile);
                tileIndex++;
                currentTile = GetTile(origin, direction * tileIndex);
            }
            return tiles;
        }

        public void PlacePlayer(Player player, Position newPosition)
        {
            if (!player)
            {
                Debug.LogError("Attempted to place null player");
                return;
            }
            
            if (!IsValidPosition(newPosition))
            {
                Debug.LogError($"Invalid position: {newPosition}");
                return;
            }
            
            var targetTile = _tilesTwoD[newPosition.X, newPosition.Y];
            if (targetTile.Player)
            {
                Debug.LogError($"Tile at {newPosition} is already occupied");
                return;
            }
            
            var oldPosition = player.CurrentTile.Location;
            if (IsValidPosition(oldPosition))
            {
                _tilesTwoD[oldPosition.X, oldPosition.Y].Player = null;
            }

            player.CurrentTile = targetTile;
            targetTile.Player = player;
        }
    }
}