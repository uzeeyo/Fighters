using UnityEngine;

namespace Fighters.Match
{
    public class TileGrid : MonoBehaviour
    {
        const int gridXSize = 3;
        const int gridYSize = 3;

        [SerializeField] private GameObject _tileParentObject;

        private GameObject[] _tileObjects;
        private Tile[,] _tilesTwoD = new Tile[gridXSize, gridYSize];

        private void Awake()
        {
            _tileObjects = new GameObject[gridXSize * gridYSize];
            for (int i = 0; i < gridXSize * gridYSize; i++)
            {
                _tileObjects[i] = _tileParentObject.transform.GetChild(i).gameObject;
            }

            int index = 0;
            for (int x = 0; x < gridXSize; x++)
            {
                for (int y = 0; y < gridYSize; y++)
                {
                    var tile = transform.GetChild(index).GetComponent<Tile>();
                    tile.transform.position = _tileObjects[index].transform.position;
                    tile.TileObject = _tileObjects[index];
                    _tilesTwoD[x, y] = tile;
                    index++;
                }
            }
        }


        public Tile GetTile(Vector2 start, Vector2 delta)
        {
            int x = (int)start.x + (int)delta.x;
            int y = (int)start.y + (int)delta.y;

            if (x < 0 || x > 2 || y < 0 || y > 2) return null;
            return _tilesTwoD[x, y];
        }
    }
}
