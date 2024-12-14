using System;
using Fighters.Match.Players;
using Fighters.Match.Spells;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match
{
    public class Tile : MonoBehaviour
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissiveColor");

        private Color _originalGlowColor;
        private Material _glowMaterial;
        private Material _stateMaterial;
        private TileStateHandler _stateHandler;

        public Position Position { get; private set; }
        public Player Player { get; set; }
        public GameObject TileObject { get; set; }
        public ITileState State => _stateHandler.State;

        [field: SerializeField] public Side PlayerSide { get; set; }

        public void Init(Position position)
        {
            Position = position;
            _stateMaterial = TileObject.GetComponent<MeshRenderer>().materials[0];
            _glowMaterial = TileObject.GetComponent<MeshRenderer>().materials[1];
            _originalGlowColor = _glowMaterial.GetColor(EmissionColor);
            _stateHandler = new(_stateMaterial);
        }

        public void ChangeState(SpellData spellData)
        {
            _stateHandler.ChangeState(spellData);
        }
        
        public void Step(Player player)
        {
            if (!_stateHandler.State.IsSteppable);

            if (State is IBuffTileState buffState)
            {
                buffState.HandleStep(player);
            }
        }
        
        //delete??
        public async void HighLight()
        {
            _glowMaterial.SetColor(EmissionColor, Color.red * 2f);
            await Awaitable.WaitForSecondsAsync(1f);
            _glowMaterial.SetColor(EmissionColor, _originalGlowColor);
        }
    }
}