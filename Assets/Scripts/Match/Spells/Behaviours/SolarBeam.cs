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
            _range = data.Range;
            _damage = data.Damage;
        }

        public override IEnumerator Cast(Tile origin)
        {
            yield return new WaitForSeconds(CastTime);
            var targetTile = origin.Grid.GetTile(origin.Location, new Vector2(_range, 0));
            transform.position = targetTile.transform.position;
        }
    }
}