using System.Collections;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public class SolarLaser : Spell
    {
        public override void Init(SpellData data)
        {
            base.Init(data);
        }

        public override IEnumerator Cast(Tile origin)
        {
            _vfx.Stop();
            _vfx.SetFloat("CastTime", CastTime);
            _vfx.Play();
            var pos = origin.transform.position;
            pos.y = 2.2f;
            transform.position = pos;
            var duration = _vfx.GetFloat("Duration");

            yield return new WaitForSeconds(CastTime);
            StartCoroutine(Shaker.ShakeForSeconds(0.15f, duration));
        }
    }
}