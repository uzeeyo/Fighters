using UnityEngine;

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
            Poisoned
        }

        public TileState State { get; set; }
        public GameObject TileObject { get; set; }

        private void Start()
        {
            State = TileState.None;
        }

        public void SetState(TileState state)
        {
            State = state;
        }
    }
}