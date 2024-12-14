using System;
using System.Collections.Generic;
using Fighters.Buffs;

namespace Fighters.Match.Spells
{
    public static class SpellFactory
    {
        private static readonly Dictionary<SpellType, ISpellEffectFactory> _spellFactories = new()
        {
            { SpellType.Damage, new DamageEffectFactory() },
            { SpellType.Heal, new HealEffectFactory() },
            { SpellType.Buff, new BuffEffectFactory() }
        };

        public static Spell Get(SpellData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Prefab == null)
                throw new ArgumentException("Spell prefab is null", nameof(data));

            if (!_spellFactories.TryGetValue(data.SpellType, out var factory))
                throw new ArgumentException($"No factory found for spell type: {data.SpellType}", nameof(data));

            var spell = UnityEngine.Object.Instantiate(data.Prefab);
            var effect = factory.Get(data);
            spell.Init(data, effect);

            return spell;
        }
    }

    
    //Effect factory needs to split off in its own thing
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

    public class BuffEffectFactory : ISpellEffectFactory
    {
        private Dictionary<BuffType, IBuffFactory> _buffFactories = new()
        {
            { BuffType.Root, new RootBuffFactory() },
            { BuffType.Poison, new PoisonEffectFactory() },
            { BuffType.Burn, new BurnEffectFactory() }
        };
        
        public ISpellEffect Get(SpellData data)
        {
            if (data is BuffData buffData)
            {
                return _buffFactories[buffData.BuffType].Get(buffData);
            }

            throw new ArgumentException($"Unknown spell type: {data.GetType()}", nameof(data));
        }
        
        private interface IBuffFactory
        {
            public BuffEffect Get(BuffData buffData);
        }
        
        private class RootBuffFactory : IBuffFactory
        {
            public BuffEffect Get(BuffData buffData) => new RootEffect(buffData);
        }

        private class PoisonEffectFactory : IBuffFactory
        {
            public BuffEffect Get(BuffData buffData) => new PoisonEffect(buffData);
        }

        private class BurnEffectFactory : IBuffFactory
        {
            public BuffEffect Get(BuffData buffData) => new BurnEffect(buffData);
        }
    }
}