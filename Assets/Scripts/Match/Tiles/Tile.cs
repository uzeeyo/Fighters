using Fighters.Match.Players;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match
{
    public class Tile : MonoBehaviour
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissiveColor");

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

        private Color _originalGlowColor;
        private Material _glowMaterial;

        public TileState State { get; private set; }
        public Position Location { get; private set; }

        public Player Player { get; set; }
        public GameObject TileObject { get; set; }

        [field: SerializeField] public Side PlayerSide { get; set; }

        private void Awake()
        {
            State = TileState.None;
        }

        public void Init(Position position)
        {
            Location = position;
            _glowMaterial = TileObject.GetComponent<MeshRenderer>().materials[1];
            _originalGlowColor = _glowMaterial.GetColor(EmissionColor);
        }

        public void SetState(TileState state)
        {
            State = state;
        }
        
        public async void HighLight()
        {
            _glowMaterial.SetColor(EmissionColor, Color.red * 2f);
            await Awaitable.WaitForSecondsAsync(1f);
            _glowMaterial.SetColor(EmissionColor, _originalGlowColor);
        }
    }
}