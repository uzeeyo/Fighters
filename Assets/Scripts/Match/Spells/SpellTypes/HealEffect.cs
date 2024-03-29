using Fighters.Match.Players;

namespace Fighters.Match.Spells
{
    public class HealEffect : SpellEffect
    {
        private float _healAmount;

        public float HealAmount => _healAmount;

        public override void SetData(SpellData data)
        {
            _healAmount = data.ModifierAmount;
        }

        public override void Apply(PlayerStats stats)
        {
            stats.Heal(_healAmount);
        }
    }
}