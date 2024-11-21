using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fighters.Match.Spells
{
    public static class SpellFactory
    {
        private static readonly Dictionary<SpellType, ISpellEffectFactory> _spellFactories = new()
        {
            { SpellType.Damage, new DamageEffectFactory() },
            { SpellType.Heal, new HealEffectFactory() },
            //{ SpellType.Buff, new BuffEffectFactory() }
        };

        public static Spell Get(SpellData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Prefab == null)
                throw new ArgumentException("Spell prefab is null", nameof(data));

            if (!_spellFactories.TryGetValue(data.SpellType, out var factory))
                throw new ArgumentException($"No factory found for spell type: {data.SpellType}", nameof(data));

            var spell = Object.Instantiate(data.Prefab);
            var effect = factory.Get(data);  // Remove the asterisk
            spell.Init(data, effect);

            return spell;
        }
    }

    public interface ISpellEffectFactory
    {
        ISpellEffect Get(SpellData data);
    }

    public class DamageEffectFactory : ISpellEffectFactory
    {
        public ISpellEffect Get(SpellData data)
        {
            return new DamageEffect(data);
        }
    }

    public class HealEffectFactory : ISpellEffectFactory
    {
        public ISpellEffect Get(SpellData data)
        {
            return new HealEffect(data);
        }
    }

    // public class BuffEffectFactory : ISpellEffectFactory
    // {
    //     public SpellEffect Get(SpellData data)
    //     {
    //         return new BuffEffect();
    //     }
    // }
}