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

        protected virtual void Awake() => Destroy(gameObject, 2f);

        public void Init(SpellData data, ISpellEffect effect)
        {
            Data = data;
            Effect = effect;
        }

        public async void Cast(Player caster)
        {
            _caster = caster;
            caster.AnimationHandler.Play(Data.AnimationName);
            await Awaitable.WaitForSecondsAsync(Data.CastTime);
            Targeter.Target(caster, this);
            
            transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnImpact(other, transform.position, gameObject);
        }

        public void OnImpact(Collider other, Vector3 hitPoint, GameObject projectile)
        {
            if (other && other.TryGetComponent(out Player player) && player != _caster)
            {
                Effect.Apply(player.Stats);
            }

            if (Data is DamageData damageData && damageData.HitEffect)
            {
                var hitEffect = new GameObject("HitEffect", typeof(VisualEffect), typeof(HitEffect));
                Instantiate(hitEffect);
                hitEffect.transform.position = hitPoint;
                hitEffect.GetComponent<VisualEffect>().visualEffectAsset = damageData.HitEffect;
            }
            Destroy(projectile);
        }
    }
}