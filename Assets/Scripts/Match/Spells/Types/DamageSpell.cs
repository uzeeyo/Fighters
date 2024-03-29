namespace Fighters.Match.Spells
{
    public class DamageSpell : SpellTypeComponent
    {
        private float _damage;

        public float Damage => _damage;

        public override void SetData(SpellData data)
        {
            _damage = data.Damage;
        }
    }
}