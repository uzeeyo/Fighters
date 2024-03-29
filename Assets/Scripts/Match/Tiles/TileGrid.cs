using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fighters.Match
{
    public class TileGrid : MonoBehaviour
    {
        const int gridXSize = 3;
        const int gridYSize = 3;

        [SerializeField] private GameObject _tileParentObject;
        [SerializeField] private Owner _owner;

        private Tile[,] _tilesTwoD = new Tile[gridXSize, gridYSize];
        private TileGrid _opponentGrid;

        public Owner Owner => _owner;

        private void Awake()
        {
            var tileObjects = new GameObject[gridXSize * gridYSize];
            for (int i = 0; i < gridXSize * gridYSize; i++)
            {
                tileObjects[i] = _tileParentObject.transform.GetChild(i).gameObject;
            }

            int index = 0;
            for (int y = gridYSize - 1; y >= 0; y--)
            {
                for (int x = 0; x < gridYSize; x++)
                {
                    var tile = transform.GetChild(index).GetComponent<Tile>();
                    tile.transform.position = tileObjects[index].transform.position;
                    tile.TileObject = tileObjects[index];
                    tile.Init(new Vector2(x, y));
                    _tilesTwoD[x, y] = tile;
                    index++;
                }
            }

            _opponentGrid = FindObjectsByType<TileGrid>(FindObjectsSortMode.InstanceID).ToList().Where(t => t != this).FirstOrDefault();
        }


        public Tile GetTile(Vector2 start, Vector2 delta)
        {
            int x = (int)start.x + (int)delta.x;
            int y = (int)start.y + (int)delta.y;

            //(2, 1) -> (2, 1)

            if (x < 0 || y < 0 || y > 2) return null;

            if (x > 2)
            {
                return _opponentGrid.GetTile(new Vector2(0, y), new Vector2(x - 3, 0));
            }

            return _tilesTwoD[x, y];
        }

        public List<Tile> GetTileStraightRange(Vector2 origin, Vector2 range)
        {
            List<Tile> tiles = new List<Tile>();
            var currentTile = GetTile(origin, Vector2.right);
            int tileIndex = 1;

            while (currentTile != null && tileIndex < range.x)
            {
                tiles.Add(currentTile);
                tileIndex++;
                currentTile = GetTile(origin, new Vector2(tileIndex, 0));
            }
            return tiles;
        }

    }
}
