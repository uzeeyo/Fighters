using Fighters.Match.Players;
using Fighters.Match.Spells;

namespace Fighters.Match
{
    public class BurntTileState : IBuffTileState
    {
        public BurntTileState(BuffEffect buffEffect, float duration)
        {
            _buffEffect = buffEffect;
            _duration = duration;
        }
        
        private readonly BuffEffect _buffEffect;
        private readonly float _duration;
        
        public float Duration => _duration;
        public bool IsSteppable => true;
        public string ShaderProperty => "Burnt";
        
        public void HandleStep(Player player)
        {
            _buffEffect.Apply(player.Stats);
        }
    }
}