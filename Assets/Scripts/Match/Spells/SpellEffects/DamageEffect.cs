using Fighters.Match.Players;

namespace Fighters.Match.Spells
{
    public class DamageEffect : ISpellEffect
    {
        public DamageEffect(SpellData data)
        {
            _damage = ((DamageData)data).DamageAmount;
        }
        
        private readonly float _damage;

        public void Apply(PlayerStats stats)
        {
            stats.TakeDamage(_damage);
        }
    }
}