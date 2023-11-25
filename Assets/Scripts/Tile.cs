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
            Blocked
        }


        [SerializeField] private Vector3 _fighterPosition;

        public TileState State { get; set; }

        public Vector3 FighterPosition { get => _fighterPosition; }

        private void Start()
        {
            State = TileState.None;
        }

    }
}