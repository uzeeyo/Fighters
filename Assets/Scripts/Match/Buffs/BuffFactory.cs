using System.Collections.Generic;

namespace Fighters.Buffs
{
    public static class BuffFactory
    {
        private static Dictionary<BuffType, IBuffFactory> _buffFactories = new()
        {
            { BuffType.Root, new RootBuffFactory() }
        };
        
        public static Buff Get(BuffData buffData)
        {
            return _buffFactories[buffData.BuffType].Get(buffData);
        }
    }

    public interface IBuffFactory
    {
        public Buff Get(BuffData buffData);
    }

    public class RootBuffFactory : IBuffFactory
    {
        public Buff Get(BuffData buffData)
        {
            return new RootBuff(buffData);
        }
    }
}