using Fighters.Buffs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Fighters.Match.Spells;

namespace Match.Player
{
    public class BuffHandler
    {
        private readonly List<BuffEffect> _buffs = new();

        private object _buffLock = new();
        private bool _checking;

        private List<BuffType> _uniqueBuffs = new()
        {
            BuffType.Shield,
            BuffType.Burn,
        };

        public event Action<BuffEffect> BuffAdded;
        public event Action<BuffEffect> BuffRemoved;

        public bool TryGetBuff(BuffType buffType, out BuffEffect buff)
        {
            buff = _buffs.FirstOrDefault(x => x.BuffType == buffType);
            return buff != null;
        }

        public bool TryGetBuffs(BuffType buffType, out IEnumerable<BuffEffect> buffs)
        {
            buffs = _buffs.Where(x => x.BuffType == buffType);
            return buffs.Any();
        }

        public void Add(BuffEffect effect)
        {
            Debug.Log("Applying buff");
            if (_uniqueBuffs.Contains(effect.BuffType) && _buffs.Any(x => x.BuffType == effect.BuffType))
            {
                Remove(effect.BuffType);
            }

            lock (_buffLock)
            {
                _buffs.Add(effect);
            }

            effect.Activate();
            BuffAdded?.Invoke(effect);
            if (!_checking) CheckForExpired();
        }

        public void Remove(BuffType buffType)
        {
            var buff = _buffs.First(x => x.BuffType == buffType);
            Remove(buff);
        }

        private void Remove(BuffEffect buff)
        {
            try
            {
                lock (_buffLock)
                {
                    _buffs.Remove(buff);
                }

                buff.Deactivate();
                BuffRemoved?.Invoke(buff);
            }
            catch
            {
                Debug.LogError($"Failed to remove buff");
            }
        }

        public void RemoveAll()
        {
            foreach (var buff in _buffs)
            {
                Remove(buff);
            }
        }

        private async void CheckForExpired()
        {
            try
            {
                while (_buffs.Any())
                {
                    for (var i = _buffs.Count - 1; i >= 0; i--)
                    {
                        if (!(_buffs[i].TimeRemaining < 0)) continue;

                        Remove(_buffs[i]);
                    }

                    await Awaitable.NextFrameAsync();
                }
            }
            finally
            {
                _checking = false;
            }
        }
    }
}