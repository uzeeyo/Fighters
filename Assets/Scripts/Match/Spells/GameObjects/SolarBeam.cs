using System.Collections;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public class SolarBeam : Spell
    {
        private float _range;
        private float _damage;

        public float Damage => _damage;

        public override void Init(SpellData data)
        {
            base.Init(data);
            _range = data.Range;
            _damage = data.ModifierAmount;
        }

        public override IEnumerator Cast(Tile origin)
        {
            _vfx.Stop();
            yield return new WaitForSeconds(CastTime);
            _vfx.Play();
            var targetTile = origin.Grid.GetTile(origin.Location, new Vector2(_range, 0));
            transform.position = targetTile.transform.position;
        }
    }
}