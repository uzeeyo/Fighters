using UnityEngine;
using UnityEngine.VFX;

namespace Fighters.Buffs
{
    public abstract class Buff
    {
        public Buff(BuffData buffData)
        {
            _duration = buffData.Duration;
            BuffType = buffData.BuffType;
            _timeStarted = Time.time;
        }
        
        private float _timeStarted;
        private float _duration;
        
        public BuffType BuffType { get; protected set; }
        public float TimeRemaining => _duration - (Time.time - _timeStarted);


        public abstract void Activate();
        public abstract void Deactivate();
    }
}