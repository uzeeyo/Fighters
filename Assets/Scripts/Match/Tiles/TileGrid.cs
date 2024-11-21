using System.Collections.Generic;
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
                    tile.Init(new Vector2(x, y));
                    tile.PlayerSide = x > 2 ? Side.Opponent : Side.Self;
                    _tilesTwoD[x, y] = tile;
                    index++;
                }
            }
        }

        public bool IsValidPosition(Vector2 position)
        {
            int x = (int)position.x;
            int y = (int)position.y;
            return x >= 0 && x < GRID_X_SIZE && y >= 0 && y < GRID_Y_SIZE;
        }

        public Tile GetTile(Vector2 start, Vector2 delta)
        {
            int x = (int)start.x + (int)delta.x;
            int y = (int)start.y + (int)delta.y;

            if (!IsValidPosition(new Vector2(x, y))) return null;

            return _tilesTwoD[x, y];
        }

        public List<Tile> GetTilesInDirection(Vector2 origin, Vector2 direction, int range)
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

        public List<Tile> GetTileStraightRange(Vector2 origin, Vector2 range)
        {
            return GetTilesInDirection(origin, Vector2.right, (int)range.x);
        }

        public void PlacePlayer(Player player, Vector2 newPosition)
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
            
            var targetTile = _tilesTwoD[(int)newPosition.x, (int)newPosition.y];
            if (targetTile.Player)
            {
                Debug.LogError($"Tile at {newPosition} is already occupied");
                return;
            }
            
            var oldPosition = player.CurrentTile.Location;
            if (IsValidPosition(oldPosition))
            {
                _tilesTwoD[(int)oldPosition.x, (int)oldPosition.y].Player = null;
            }

            player.CurrentTile = targetTile;
            targetTile.Player = player;
        }
    }
}