using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match
{
    public class Tile : MonoBehaviour
    {
        public enum TileState
        {
            None,
            Slow,
            Root,
            Blocked,
            Burned,
            Poisoned,
            Frozen,
        }

        private TileGrid _grid;
        private Vector2 _location;
        private VisualEffect _vfx;


        public TileState State { get; set; }
        public Vector2 Location => _location;
        public bool HasPlayer => GetComponentInChildren<Players.Player>() != null;
        public GameObject TileObject { get; set; }
        public TileGrid Grid => GetComponentInParent<TileGrid>();
        public VisualEffect Vfx => _vfx;

        private void Awake()
        {
            State = TileState.None;
            _grid = GetComponentInParent<TileGrid>();
        }

        public void Init(Vector2 location)
        {
            _location = location;
        }

        public void SetState(TileState state)
        {
            State = state;
        }
    }
}