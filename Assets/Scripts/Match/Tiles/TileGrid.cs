using UnityEngine;

namespace Fighters.Match
{
    public class TileGrid : MonoBehaviour
    {

        private static readonly int gridXSize = 3;
        private static readonly int gridYSize = 3;
        private Tile[] _tileArray;


        private Tile[,] _tiles = new Tile[gridXSize, gridYSize];

        private void Awake()
        {
            _tileArray = GetComponentsInChildren<Tile>();
            int index = 0;

            for (int x = 0; x < gridXSize; x++)
            {
                for (int y = 0; y < gridYSize; y++)
                {
                    _tiles[x, y] = _tileArray[index++];
                }
            }
        }


        public Tile GetTile(GridPosition start, GridPosition delta)
        {
            var x = start.X + delta.X;
            var y = start.Y + delta.Y;

            if (x < 0 || x > 2 || y < 0 || y > 2) return null;

            return _tiles[x, y];
        }
    }
}
