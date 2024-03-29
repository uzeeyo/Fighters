using System.Collections;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public class QuickHeal : Spell
    {
        private float _healAmount;

        public float HealAmount => _healAmount;

        public override void Init(SpellData data)
        {
            base.Init(data);
            _healAmount = data.ModifierAmount;
        }

        public override IEnumerator Cast(Tile origin)
        {
            transform.position = origin.transform.position;
            _vfx.Stop();
            yield return new WaitForSeconds(CastTime);
            _vfx.Play();

        }
    }
}