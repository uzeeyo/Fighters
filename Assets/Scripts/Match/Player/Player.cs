using UnityEngine;

namespace Fighters.Match.Players
{
    public class Player : MonoBehaviour
    {
        private PlayerStats _stats;
        [SerializeField] private TileGrid _grid;

        public PlayerStats Stats => _stats;
        public TileGrid Grid => _grid;
        public Tile CurrentTile { get; set; }


        private void Start()
        {
            _stats = GetComponent<PlayerStats>();
        }

        public void SetLocation(Vector2 location)
        {
            //_location = location;
        }
    }
}