namespace Fighters.Match.Spells
{
    public class DamageSpell : Spell
    {
        private float _damage;

        public float Damage => _damage;

        public override void Init(SpellData data)
        {
            base.Init(data);
            _damage = data.Damage;
        }

        public override void Cast(Tile origin)
        {
            base.Cast(origin);
        }
    }
}