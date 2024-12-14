using System;
using System.Collections.Generic;
using Fighters.Match.Spells;
using UnityEngine;

namespace Fighters.Match
{
    public static class TileStateFactory
    {
        private static readonly ITileState s_defaultState = new DefaultTileState();
        //TODO:use DI or something, there shouldn't be 2 instances of this here and SpellFactory
        private static readonly BuffEffectFactory s_buffEffectFactory = new();
        private static Dictionary<TileState, ITileStateFactory> s_factories = new()
        {
            { TileState.Blocked, new BlockedStateFactory() },
            { TileState.Burnt, new BurntStateFactory() },
        };

        public static ITileState GetDefault() => s_defaultState;
        
        public static ITileState Get(SpellData spellData)
        {
            if (s_factories.TryGetValue(spellData.TileState, out var factory))
            {
                return factory.CreateState(spellData);
            }

            throw new NotImplementedException();
        }

        private interface ITileStateFactory
        {
            ITileState CreateState(SpellData spellData);
        }

        private class BlockedStateFactory : ITileStateFactory
        {
            public ITileState CreateState(SpellData spellData) => new BrokenTileState(spellData.TileStateDuration);
        }

        private class BurntStateFactory : ITileStateFactory
        {
            public ITileState CreateState(SpellData spellData)
            {
                var buffEffect = s_buffEffectFactory.Get(spellData) as BuffEffect;
                return new BurntTileState(buffEffect, spellData.TileStateDuration);
            }
        }
    }
}