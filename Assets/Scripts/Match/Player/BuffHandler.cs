using Fighters.Buffs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace Match.Player
{
    public class BuffHandler
    {
        private readonly List<Buff> _buffs = new();

        private object _buffLock = new();
        private bool _checking;

        private List<BuffType> _uniqueBuffs = new()
        {
            BuffType.Shielded,
        };

        public event Action<Buff> BuffAdded;
        public event Action<Buff> BuffRemoved;

        public bool TryGetBuff(BuffType buffType, out Buff buff)
        {
            buff = _buffs.FirstOrDefault(x => x.BuffType == buffType);
            return buff != null;
        }

        public bool TryGetBuffs(BuffType buffType, out IEnumerable<Buff> buffs)
        {
            buffs = _buffs.Where(x => x.BuffType == buffType);
            return buffs.Any();
        }

        public void Add(BuffData buffData)
        {
            if (_uniqueBuffs.Contains(buffData.BuffType)) return;
            var buff = BuffFactory.Get(buffData);
            lock (_buffLock)
            {
                _buffs.Add(buff);
            }

            BuffAdded?.Invoke(buff);
            if (!_checking) CheckForExpired();
        }

        public void Remove(BuffType buffType)
        {
            _buffs.RemoveAll(x => x.BuffType == buffType);
        }

        public void RemoveAll()
        {
            foreach (var buff in _buffs)
            {
                BuffRemoved?.Invoke(buff);
            }
            lock (_buffLock)
            {
                _buffs.Clear();
            }
        }

        private async void CheckForExpired()
        {
            if (_checking) return;

            _checking = true;
            try
            {
                while (_buffs.Any())
                {
                    for (var i = _buffs.Count - 1; i >= 0; i--)
                    {
                        if (!(_buffs[i].TimeRemaining < 0)) continue;

                        BuffRemoved?.Invoke(_buffs[i]);
                        lock (_buffLock)
                        {
                            _buffs.RemoveAt(i);
                        }
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