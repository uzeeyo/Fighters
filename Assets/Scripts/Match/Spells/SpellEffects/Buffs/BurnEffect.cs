using Fighters.Match.Spells;
using UnityEngine;

namespace Fighters.Buffs
{
    public class BurnEffect : BuffEffect
    {
        public BurnEffect(BuffData buffData) : base(buffData)
        {
            _duration = buffData.BuffDuration;
        }

        private const float DPS = 3f;
        private readonly float _duration;
        private bool _active;
        
        public override async void Activate()
        {
            float timer = 0;
            _active = true;
            
            while (timer < _duration && _active)
            {
                if (_playerStats.CurrentHealth <= 0)
                {
                    Deactivate();
                    return;
                }
                var damageForFrame = DPS * Time.deltaTime;
                _playerStats.TakeDamage(damageForFrame);
                timer += Time.deltaTime;

                await Awaitable.NextFrameAsync();
            }
        }

        public override void Deactivate()
        {
            _active = false;
        }
    }
}