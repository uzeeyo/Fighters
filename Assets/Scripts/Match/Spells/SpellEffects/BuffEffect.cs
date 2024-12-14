using Fighters.Buffs;
using Fighters.Match.Players;
using UnityEngine;

namespace Fighters.Match.Spells
{
    public abstract class BuffEffect : ISpellEffect
    {
        protected BuffEffect(BuffData buffData)
        {
            Duration = buffData.BuffDuration;
            BuffType = buffData.BuffType;
            _timeStarted = Time.time;
            Icon = buffData.Icon;
        }

        private float _timeStarted;
        
        protected PlayerStats _playerStats;

        public BuffType BuffType { get; private set; }
        public float Duration { get; }
        public Sprite Icon { get; private set; }

        public float TimeRemaining => Duration - (Time.time - _timeStarted);

        public void Apply(PlayerStats stats)
        {
            _playerStats = stats;
            _playerStats.AddBuff(this);
        }

        public abstract void Activate();
        public abstract void Deactivate();
    }
}