using Fighters.Match.Players;

namespace Fighters.Match.Spells
{
    public class HealEffect : ISpellEffect
    {
        public HealEffect(SpellData data)
        {
            _healAmount = ((HealData)data).HealAmount;
        }
        
        private readonly float _healAmount;

        public void Apply(PlayerStats stats)
        {
            stats.Heal(_healAmount);
        }
    }
}