using System;
using Fighters.Match.Spells;
using Fighters.Match.Players;
using UnityEngine;

namespace Fighters.Match
{
    public class Spell : MonoBehaviour
    {
        private Player _caster;
        public SpellData Data { get; private set; }
        public ISpellEffect Effect { get; private set; }

        protected virtual void Awake() => Destroy(gameObject, 3f);

        public void Init(SpellData data, ISpellEffect effect)
        {
            Data = data;
            Effect = effect;
        }

        public async void Cast(Player caster)
        {
            _caster = caster;
            Targeter.Target(caster, this);
            caster.PlayAnimation(Data.AnimationName);
            await Awaitable.WaitForSecondsAsync(Data.CastTime);
            transform.SetParent(null);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) && player != _caster)
            {
                Effect.Apply(player.Stats);
            }
            Destroy(gameObject);
        }
    }
}