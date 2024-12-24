using System;
using Fighters.Match.Spells;
using Fighters.Match.Players;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match.Spells
{
    public class Spell : MonoBehaviour
    {
        private Player _caster;
        public SpellData Data { get; private set; }
        public ISpellEffect Effect { get; private set; }

        private void Awake() => Destroy(gameObject, 5f);

        public void Init(SpellData data, ISpellEffect effect)
        {
            Data = data;
            Effect = effect;
        }

        public void OnImpact(Projectile projectile, Collider other, Vector3 hitPoint)
        {
            Destroy(projectile.gameObject);
            if (other && other.TryGetComponent(out Player player) && player != _caster)
            {
                Effect.Apply(player.Stats);
            }

            if (Data.ShakesOnImpact)
            {
                Shaker.Shake(Data.ShakeStrength, Data.ShakeDuration);
            }

            //all spells should be able to make hit effects
            if (Data is DamageData damageData && damageData.HitEffect)
            {
                var hitEffect = new GameObject("HitEffect", typeof(VisualEffect), typeof(HitEffect));
                Instantiate(hitEffect);
                hitEffect.transform.position = hitPoint;
                hitEffect.GetComponent<VisualEffect>().visualEffectAsset = damageData.HitEffect;
            }
        }
    }
}