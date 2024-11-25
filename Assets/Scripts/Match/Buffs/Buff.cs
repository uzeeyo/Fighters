using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Buffs
{
    public abstract class Buff
    {
        public Buff(BuffData buffData)
        {
            Duration = buffData.Duration;
            BuffType = buffData.BuffType;
            _timeStarted = Time.time;
            Icon = buffData.Icon;
        }
        
        private float _timeStarted;

        public BuffType BuffType { get; protected set; }
        public float Duration { get; }
        public Sprite Icon { get; private set; }

        public float TimeRemaining => Duration - (Time.time - _timeStarted);


        public abstract void Activate();
        public abstract void Deactivate();
    }
}