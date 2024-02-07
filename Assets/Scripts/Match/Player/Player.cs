using UnityEngine;

namespace Fighters.Match.Player
{
    public class Player : MonoBehaviour
    {
        private PlayerStats _stats;

        [SerializeField] private TileGrid _grid;

        public PlayerStats Stats => _stats;
        public TileGrid Grid => _grid;


        private void Start()
        {
            _stats = GetComponent<PlayerStats>();
        }

        public void SetGrid(TileGrid grid)
        {
            _grid = grid;
        }
    }
}