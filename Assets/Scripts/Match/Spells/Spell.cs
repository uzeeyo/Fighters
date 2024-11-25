using System;
using Fighters.Match.Spells;
using Fighters.Match.Players;
using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Match
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
            Targeter.Target(caster, this);
            
            await Awaitable.WaitForSecondsAsync(Data.CastTime);
            
            transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) && player != _caster)
            {
                Effect.Apply(player.Stats);
                if (Data is DamageData damageData && damageData.HitEffect)
                {
                    var hitEffect = new GameObject("HitEffect", typeof(VisualEffect), typeof(HitEffect));
                    Instantiate(hitEffect);
                    hitEffect.transform.position = transform.position;
                    hitEffect.GetComponent<VisualEffect>().visualEffectAsset = damageData.HitEffect;
                }
            }
            Destroy(gameObject);
        }
    }
}