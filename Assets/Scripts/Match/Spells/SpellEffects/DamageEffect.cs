using Fighters.Match.Players;

namespace Fighters.Match.Spells
{
    public class DamageEffect : SpellEffect
    {
        private float _damage;

        public float Damage => _damage;

        public override void Init(SpellData data)
        {
            _damage = data.ModifierAmount;
        }

        public override void Apply(PlayerStats stats)
        {
            stats.TakeDamage(_damage);
        }
    }
}